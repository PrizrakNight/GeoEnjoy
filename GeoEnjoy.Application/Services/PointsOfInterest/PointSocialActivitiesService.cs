using FluentResults;
using GeoEnjoy.Application.Errors;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Application.Services.SocialActivities;
using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Services.PointsOfInterest
{
    public class PointSocialActivitiesService : SocialActivitiesBaseService
    {
        public PointSocialActivitiesService(ICancellationTokenProvider tokenProvider,
            IGeoEnjoyRepository repository,
            ICurrentUserProvider currentUser) : base(tokenProvider, repository, currentUser)
        {
        }

        protected override SocialEntityType GetSocialEntityType()
        {
            return SocialEntityType.PointOfInterest;
        }

        protected override async Task<Result> ExistsAsync(Guid id)
        {
            var pointExists = await Repository.PointsOfInterest.ExistsByIdAsync
            (
                id: id,
                cancellationToken: TokenProvider.CancellationToken
            );

            if (!pointExists)
            {
                return Result.Fail(GeoEnjoyErrors.PointOfInterestNotFound());
            }

            return Result.Ok();
        }
    }
}
