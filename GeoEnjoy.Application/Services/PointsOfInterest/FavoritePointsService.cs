using FluentResults;
using GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Errors;
using GeoEnjoy.Application.Exceptions;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Application.Sortings;
using GeoEnjoy.Domain;
using Spec = GeoEnjoy.Application.Specifications.FavoritePointOfInterestSpecifications;

namespace GeoEnjoy.Application.Services.PointsOfInterest;

public class FavoritePointsService(
    ICurrentUserProvider currentUser,
    IGeoEnjoyRepository repository,
    ICancellationTokenProvider tokenProvider,
    IMappingService mapping) : IFavoritePointsService
{
    public async Task<Result> AddAsync(Guid pointId)
    {
        var currentUserId = currentUser.Id;

        var condition = Spec.ByUser(currentUserId) & Spec.ByPoint(pointId);

        var favoriteExists = await repository.FavoritePointsOfInterest.ExistsBySpecAsync
        (
            spec: condition,
            cancellationToken: tokenProvider.CancellationToken
        );

        if (favoriteExists)
        {
            return Result.Fail(GeoEnjoyErrors.FavoriteAlredyExists());
        }

        var favorite = new FavoritePointOfInterest
        {
            PointId = pointId,
            UserId = currentUserId
        };

        repository.FavoritePointsOfInterest.Add(favorite);

        await repository.SaveChangesAsync(tokenProvider.CancellationToken);

        return Result.Ok();
    }

    public async Task<Result> RemoveAsync(Guid pointId)
    {
        repository.FavoritePointsOfInterest.DeleteBy(currentUser.Id, pointId);

        await repository.SaveChangesAsync(tokenProvider.CancellationToken);

        return Result.Ok();
    }

    public async Task<Result<List<PointOfInterestResponse>>> GetAsync(GetOwnPointsOfInterestRequest request)
    {
        var currentUserId = currentUser.Id;

        var condition = Spec.ByUser(currentUserId);

        var favoritePoints = await repository.FavoritePointsOfInterest.FindAllBySpecAsync
        (
            spec: condition,
            pagination: request.Pagination,
            sortings: request.Sorting switch
            {
                PointsOfInterestSorting.None => [],
                PointsOfInterestSorting.Newer => FavoritePointSorting.Newer,
                PointsOfInterestSorting.Older => FavoritePointSorting.Older,

                _ => throw new SortingNotImplementedException(request.Sorting.ToString())
            },
            cancellationToken: tokenProvider.CancellationToken
        );

        var favoritePointsIds = favoritePoints
            .Select(x => x.PointId)
            .Distinct()
            .ToArray();

        var pointsOfInterests = await repository.PointsOfInterest.FindByIdsAsync
        (
            ids: favoritePointsIds,
            cancellationToken: tokenProvider.CancellationToken
        );

        var response = mapping.Map<List<PointOfInterestResponse>>(pointsOfInterests);

        return Result.Ok(response);
    }
}
