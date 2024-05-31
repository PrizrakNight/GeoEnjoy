using FluentResults;
using GeoEnjoy.Application.Errors;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Services.SocialActivities;

public class SocialActivitiesService<T>(IGeoEnjoyRepository repository,
    ICancellationTokenProvider tokenProvider,
    ICurrentUserProvider currentUser,
    IEntityExpands<T> entityExpands) : ISocialActivitiesService
    where T : ISocialActivityEntity
{
    public Task<Result> DislikeAsync(Guid id)
    {
        return SetOrCreateActivityAsync(id, SocialActivityType.Dislike);
    }

    public Task<Result> LikeAsync(Guid id)
    {
        return SetOrCreateActivityAsync(id, SocialActivityType.Like);
    }

    public async Task<Result> RemoveSocialActivityAsync(Guid id)
    {
        var entitySet = repository.Set<T>();

        entityExpands.Expand(x => x.SocialActivities!);

        var foundEntity = await entitySet
            .FindByIdAsync(id, tokenProvider.CancellationToken);

        if (foundEntity == null)
            return Result.Fail(GeoEnjoyErrors.EntityNotFound<T>());

        var currentUserId = currentUser.Id;

        var foundActivity = foundEntity.SocialActivities?
            .FirstOrDefault(x => x.UserId == currentUserId);

        if (foundActivity != null)
        {
            foundEntity.SocialActivities!.Remove(foundActivity);

            await repository.SaveChangesAsync(tokenProvider.CancellationToken);
        }

        return Result.Ok();
    }

    private async Task<Result> SetOrCreateActivityAsync(Guid id,
        SocialActivityType activityType)
    {
        var entitySet = repository.Set<T>();

        entityExpands.Expand(x => x.SocialActivities!);

        var foundEntity = await entitySet
            .FindByIdAsync(id, tokenProvider.CancellationToken);

        if (foundEntity == null)
            return Result.Fail(GeoEnjoyErrors.EntityNotFound<T>());

        var currentUserId = currentUser.Id;

        var foundActivity = foundEntity.SocialActivities!
            .FirstOrDefault(x => x.UserId == currentUserId);

        if (foundActivity == null)
        {
            foundActivity = new SocialActivity
            {
                UserId = currentUserId,
                Created = DateTime.UtcNow
            };

            foundEntity.SocialActivities!.Add(foundActivity);
        }

        foundActivity.ActivityType = activityType;
        foundActivity.Updated = DateTime.UtcNow;

        entitySet.Update(foundEntity);

        await repository.SaveChangesAsync(tokenProvider.CancellationToken);

        return Result.Ok();
    }
}
