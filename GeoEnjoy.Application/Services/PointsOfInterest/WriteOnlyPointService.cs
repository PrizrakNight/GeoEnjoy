using FluentResults;
using GeoEnjoy.Application.Contracts.Request;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Errors;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain;
using PointSpec = GeoEnjoy.Application.Specifications.PointOfInterestSpecifications;
using SocialSpec = GeoEnjoy.Application.Specifications.UserSocialActivitySpecifications;

namespace GeoEnjoy.Application.Services.PointsOfInterest
{
    public class WriteOnlyPointService(
        IGeoEnjoyRepository repository,
        ICurrentUserProvider currentUser,
        ICancellationTokenProvider tokenProvider,
        IMappingService mapping) : IWriteOnlyPointService
    {
        public async Task<Result<PointOfInterestResponse>> AddAsync(CreatePointOfInterestRequest request)
        {
            var pointOfInterest = mapping.Map<PointOfInterest>(request);

            pointOfInterest.AuthorId = currentUser.Id;

            repository.PointsOfInterest.Add(pointOfInterest);

            await repository.SaveChangesAsync(tokenProvider.CancellationToken);

            var response = mapping.Map<PointOfInterestResponse>(pointOfInterest);

            return Result.Ok(response);
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var condition = PointSpec.ByAuthor(currentUser.Id) & PointSpec.ById(id);

            var foundPoint = await repository.PointsOfInterest.FindOneBySpecAsync
            (
                spec: condition,
                cancellationToken: tokenProvider.CancellationToken
            );

            if (foundPoint == null)
            {
                return Result.Fail(GeoEnjoyErrors.PointOfInterestNotFound());
            }

            repository.PointsOfInterest.Delete(foundPoint);

            await repository.SaveChangesAsync(tokenProvider.CancellationToken);

            return Result.Ok();
        }

        public async Task<Result> DislikeAsync(Guid id)
        {
            var foundPoint = await repository.PointsOfInterest.FindByIdAsync
            (
                id: id,
                cancellationToken: tokenProvider.CancellationToken
            );

            if (foundPoint == null)
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
                    ActivityType = SocialActivityType.Dislike,
                    EntityType = SocialEntityType.PointOfInterest
                };

                repository.UserSocialActivities.Add(activity);

                await repository.SaveChangesAsync(tokenProvider.CancellationToken);

                return Result.Ok();
            }

            activity.ActivityType = SocialActivityType.Dislike;
            activity.EntityType = SocialEntityType.PointOfInterest;
            activity.Created = DateTime.UtcNow;

            repository.UserSocialActivities.Update(activity);

            await repository.SaveChangesAsync(tokenProvider.CancellationToken);

            return Result.Ok();
        }

        public async Task<Result> LikeAsync(Guid id)
        {
            var foundPoint = await repository.PointsOfInterest.FindByIdAsync
            (
                id: id,
                cancellationToken: tokenProvider.CancellationToken
            );

            if (foundPoint == null)
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
                    ActivityType = SocialActivityType.Like,
                    EntityType = SocialEntityType.PointOfInterest
                };

                repository.UserSocialActivities.Add(activity);

                await repository.SaveChangesAsync(tokenProvider.CancellationToken);

                return Result.Ok();
            }

            activity.ActivityType = SocialActivityType.Like;
            activity.EntityType = SocialEntityType.PointOfInterest;
            activity.Created = DateTime.UtcNow;

            repository.UserSocialActivities.Update(activity);

            await repository.SaveChangesAsync(tokenProvider.CancellationToken);

            return Result.Ok();
        }

        public async Task<Result<PointOfInterestResponse>> SetPublicAsync(Guid id, bool isPublic)
        {
            var condition = PointSpec.ByAuthor(currentUser.Id) & PointSpec.ById(id);

            var foundPoint = await repository.PointsOfInterest.FindOneBySpecAsync
            (
                spec: condition,
                cancellationToken: tokenProvider.CancellationToken
            );

            if (foundPoint == null)
            {
                return Result.Fail(GeoEnjoyErrors.PointOfInterestNotFound());
            }

            foundPoint.IsPublic = isPublic;

            repository.PointsOfInterest.Update(foundPoint);

            await repository.SaveChangesAsync(tokenProvider.CancellationToken);

            var response = mapping.Map<PointOfInterestResponse>(foundPoint);

            return Result.Ok(response);
        }
    }
}
