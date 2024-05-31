using FluentResults;
using GeoEnjoy.Application.Errors;
using GeoEnjoy.Application.Logging;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain.Entities;
using GeoEnjoy.Globalization.Properties;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace GeoEnjoy.Application.Services.SocialActivities;

public class SocialActivitiesService<T>(IGeoEnjoyRepository repository,
    ICancellationTokenProvider tokenProvider,
    ICurrentUserProvider currentUser,
    IEntityExpands<T> entityExpands,
    ILogger<SocialActivitiesService<T>> logger) : ISocialActivitiesService
    where T : ISocialActivityEntity
{
    public Task<Result> DislikeAsync(Guid id)
    {
        return SetActivityAsync(id, SocialActivityType.Dislike);
    }

    public Task<Result> LikeAsync(Guid id)
    {
        return SetActivityAsync(id, SocialActivityType.Like);
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

            logger.LogInformation
            (
                eventId: LoggingEvents.SocialActivityDeleted,
                message: LoggingTranslations.SocialActivityDeletedByUser,
                args:
                [
                    foundActivity.ActivityType.ToString(),
                    foundActivity.UserId,
                    foundEntity.Id
                ]
            );
        }

        return Result.Ok();
    }

    private async Task<Result> SetActivityAsync(Guid id,
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

        logger.LogInformation
        (
            eventId: LoggingEvents.SocialActivitySet,
            message: LoggingTranslations.SocialActivitySetByUser,
            args:
            [
                foundActivity.ActivityType.ToString(),
                foundActivity.UserId,
                foundEntity.Id
            ]
        );

        return Result.Ok();
    }
}
