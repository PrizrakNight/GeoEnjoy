using FluentResults;
using GeoEnjoy.Application.Errors;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Application.Services.SocialActivities;
using GeoEnjoy.Domain;

namespace GeoEnjoy.Application.Services.Reviews
{
    public class ReviewSocialActivitiesService(
        ICancellationTokenProvider tokenProvider,
        IGeoEnjoyRepository repository,
        ICurrentUserProvider currentUser)
        : SocialActivitiesBaseService(tokenProvider, repository, currentUser)
    {
        protected override SocialEntityType GetSocialEntityType()
        {
            return SocialEntityType.Review;
        }

        protected override async Task<Result> ExistsAsync(Guid id)
        {
            var reviewExists = await Repository.Reviews.ExistsByIdAsync
            (
                id: id,
                cancellationToken: TokenProvider.CancellationToken
            );

            if (!reviewExists)
            {
                return Result.Fail(GeoEnjoyErrors.ReviewNotFound());
            }

            return Result.Ok();
        }
    }
}
