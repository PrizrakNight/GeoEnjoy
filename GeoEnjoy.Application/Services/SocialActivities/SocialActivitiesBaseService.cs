using FluentResults;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain.Entities;
using SocialSpec = GeoEnjoy.Application.Specifications.UserSocialActivitySpecifications;

namespace GeoEnjoy.Application.Services.SocialActivities
{
    public abstract class SocialActivitiesBaseService : ISocialActivitiesService
    {
        protected readonly ICancellationTokenProvider TokenProvider;
        protected readonly IGeoEnjoyRepository Repository;
        protected readonly ICurrentUserProvider CurrentUser;

        protected SocialActivitiesBaseService(ICancellationTokenProvider tokenProvider,
            IGeoEnjoyRepository repository,
            ICurrentUserProvider currentUser)
        {
            TokenProvider = tokenProvider;
            Repository = repository;
            CurrentUser = currentUser;
        }

        public async Task<Result> DislikeAsync(Guid id)
        {
            var existsResult = await ExistsAsync(id);

            if (existsResult.IsFailed)
            {
                return existsResult;
            }

            return await SetActivityAsync(id, SocialActivityType.Dislike);
        }

        public async Task<Result> LikeAsync(Guid id)
        {
            var existsResult = await ExistsAsync(id);

            if (existsResult.IsFailed)
            {
                return existsResult;
            }

            return await SetActivityAsync(id, SocialActivityType.Like);
        }

        private async Task<Result> SetActivityAsync(Guid id, SocialActivityType activityType)
        {
            var currentUserId = CurrentUser.Id;

            var condition = SocialSpec.ByEntityId(id) & SocialSpec.ByUserId(currentUserId);

            var activity = await Repository.UserSocialActivities.FindOneBySpecAsync
            (
                spec: condition,
                cancellationToken: TokenProvider.CancellationToken
            );

            var entityType = GetSocialEntityType();

            if (activity == null)
            {
                activity = new UserSocialActivity
                {
                    Id = id,
                    UserId = currentUserId,
                    ActivityType = activityType,
                    EntityType = entityType
                };

                Repository.UserSocialActivities.Add(activity);

                await Repository.SaveChangesAsync(TokenProvider.CancellationToken);

                return Result.Ok();
            }

            activity.ActivityType = activityType;
            activity.EntityType = entityType;
            activity.Created = DateTime.UtcNow;

            Repository.UserSocialActivities.Update(activity);

            await Repository.SaveChangesAsync(TokenProvider.CancellationToken);

            return Result.Ok();
        }

        protected abstract SocialEntityType GetSocialEntityType();

        protected virtual Task<Result> ExistsAsync(Guid id)
        {
            return Task.FromResult(Result.Ok());
        }

        public async Task<Result> RemoveSocialActivitiesAsync(Guid id)
        {
            var condition = SocialSpec.ByEntityId(id) & SocialSpec.ByUserId(CurrentUser.Id);

            var activity = await Repository.UserSocialActivities.FindOneBySpecAsync
            (
                spec: condition,
                cancellationToken: TokenProvider.CancellationToken
            );

            if (activity != null)
            {
                Repository.UserSocialActivities.Delete(activity);

                await Repository.SaveChangesAsync(TokenProvider.CancellationToken);
            }

            return Result.Ok();
        }
    }
}
