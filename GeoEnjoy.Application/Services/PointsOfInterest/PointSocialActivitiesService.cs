using FluentResults;
using GeoEnjoy.Application.Errors;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain;
using SocialSpec = GeoEnjoy.Application.Specifications.UserSocialActivitySpecifications;

namespace GeoEnjoy.Application.Services.PointsOfInterest
{
    public class PointSocialActivitiesService(
        ICurrentUserProvider currentUser,
        IGeoEnjoyRepository repository,
        ICancellationTokenProvider tokenProvider
        ) : IPointSocialActivitiesService
    {
        public Task<Result> DislikeAsync(Guid id)
        {
            return SetActivityAsync(id, SocialActivityType.Dislike);
        }

        public Task<Result> LikeAsync(Guid id)
        {
            return SetActivityAsync(id, SocialActivityType.Like);
        }

        public async Task<Result> RemoveSocialActivitiesAsync(Guid id)
        {
            var condition = SocialSpec.ByEntityId(id) & SocialSpec.ByUserId(currentUser.Id);

            var activity = await repository.UserSocialActivities.FindOneBySpecAsync
            (
                spec: condition,
                cancellationToken: tokenProvider.CancellationToken
            );

            if (activity != null)
            {
                repository.UserSocialActivities.Delete(activity);

                await repository.SaveChangesAsync(tokenProvider.CancellationToken);
            }

            return Result.Ok();
        }

        private async Task<Result> SetActivityAsync(Guid id, SocialActivityType activityType)
        {
            var pointExists = await repository.PointsOfInterest.ExistsByIdAsync
           (
               id: id,
               cancellationToken: tokenProvider.CancellationToken
           );

            if (!pointExists)
            {
                return Result.Fail(GeoEnjoyErrors.PointOfInterestNotFound());
            }

            var currentUserId = currentUser.Id;

            var condition = SocialSpec.ByEntityId(id) & SocialSpec.ByUserId(currentUserId);

            var activity = await repository.UserSocialActivities.FindOneBySpecAsync
            (
                spec: condition,
                cancellationToken: tokenProvider.CancellationToken
            );

            if (activity == null)
            {
                activity = new UserSocialActivity
                {
                    Id = id,
                    UserId = currentUserId,
                    ActivityType = activityType,
                    EntityType = SocialEntityType.PointOfInterest
                };

                repository.UserSocialActivities.Add(activity);

                await repository.SaveChangesAsync(tokenProvider.CancellationToken);

                return Result.Ok();
            }

            activity.ActivityType = activityType;
            activity.EntityType = SocialEntityType.PointOfInterest;
            activity.Created = DateTime.UtcNow;

            repository.UserSocialActivities.Update(activity);

            await repository.SaveChangesAsync(tokenProvider.CancellationToken);

            return Result.Ok();
        }
    }
}
