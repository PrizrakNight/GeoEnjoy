using FluentResults;
using GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Dto;
using GeoEnjoy.Application.Exceptions;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Application.Sortings;

using Spec = GeoEnjoy.Application.Specifications.PointOfInterestSpecifications;

namespace GeoEnjoy.Application.Services.PointsOfInterest;

public class ReadOnlyPointService(
    IGeoEnjoyRepository repository,
    IMappingService mapping,
    ICancellationTokenProvider tokenProvider,
    ICurrentUserProvider currentUser) : IReadOnlyPointService
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

        var response = mapping.Map<List<PointOfInterestResponse>>(pointsInRadius);

        return Result.Ok(response);
    }

    public async Task<Result<List<PointOfInterestResponse>>> GetOwnAsync(GetOwnPointsOfInterestRequest request)
    {
        var currentUserId = currentUser.Id;
        var condition = Spec.ByAuthor(currentUserId);

        var ownPointsOfInterest = await repository.PointsOfInterest.FindAllBySpecAsync
        (
            spec: condition,
            pagination: request.Pagination,
            sortings: request.Sorting switch
            {
                PointsOfInterestSorting.None => [],
                PointsOfInterestSorting.Newer => PointOfInterestSorting.Newer,
                PointsOfInterestSorting.Older => PointOfInterestSorting.Older,

                _ => throw new SortingNotImplementedException(request.Sorting.ToString())
            },
            cancellationToken: tokenProvider.CancellationToken
        );

        var response = mapping.Map<List<PointOfInterestResponse>>(ownPointsOfInterest);

        return Result.Ok(response);
    }
}
