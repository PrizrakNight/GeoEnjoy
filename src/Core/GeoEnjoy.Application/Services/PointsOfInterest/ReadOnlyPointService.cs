using FluentResults;
using GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Dto;
using GeoEnjoy.Application.Extensions;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain.Entities.PointOfInterests;
using Spec = GeoEnjoy.Application.Specifications.PointOfInterestSpecifications;

namespace GeoEnjoy.Application.Services.PointsOfInterest;

public class ReadOnlyPointService(
    IGeoEnjoyRepository repository,
    IMappingService mapping,
    ICancellationTokenProvider tokenProvider,
    ICurrentUserProvider currentUser,
    IEntitySortings<PointOfInterest> pointOfInterestSortings) : IReadOnlyPointService
{
    public async Task<Result<List<PointOfInterestResponse>>> GetInRadiusAsync(RadiusDto radius)
    {
        var condition = Spec.OnlyPublic() | Spec.ByAuthor(currentUser.Id);

        var pointsInRadius = await repository.PointsOfInterest.GetInRadiusAsync
        (
            radius: radius,
            spec: condition,
            cancellationToken: tokenProvider.CancellationToken
        );

        var pointsAssessments = await repository.PointsOfInterest.GetPointOfInterestsAssessmentsAsync
        (
            pointIds: pointsInRadius.Select(x => x.Id).Distinct().ToArray(),
            cancellationToken: tokenProvider.CancellationToken
        );

        var response = pointsInRadius.Select(point =>
        {
            var result = mapping.Map<PointOfInterestResponse>(point);

            var foundPointActivities = pointsAssessments
                .FirstOrDefault(activity => activity.PointId == point.Id);

            if (foundPointActivities != null)
            {
                mapping.Map(foundPointActivities, result);
            }

            return result;

        }).ToList();

        return Result.Ok(response);
    }

    public async Task<Result<List<PointOfInterestResponse>>> GetOwnAsync(GetOwnPointsOfInterestRequest request)
    {
        var currentUserId = currentUser.Id;
        var condition = Spec.ByAuthor(currentUserId);

        pointOfInterestSortings.UseSortingWay(request.Sorting);

        var ownPointsOfInterest = await repository.PointsOfInterest.FindAllBySpecAsync
        (
            spec: condition,
            pagination: request.Pagination,
            cancellationToken: tokenProvider.CancellationToken
        );

        var pointsAssessments = await repository.PointsOfInterest.GetPointOfInterestsAssessmentsAsync
        (
            pointIds: ownPointsOfInterest.Select(x => x.Id).Distinct().ToArray(),
            cancellationToken: tokenProvider.CancellationToken
        );

        var response = ownPointsOfInterest.Select(point =>
        {
            var result = mapping.Map<PointOfInterestResponse>(point);

            var foundPointActivities = pointsAssessments
                .FirstOrDefault(activity => activity.PointId == point.Id);

            if (foundPointActivities != null)
            {
                mapping.Map(foundPointActivities, result);
            }

            return result;

        }).ToList();

        return Result.Ok(response);
    }
}
