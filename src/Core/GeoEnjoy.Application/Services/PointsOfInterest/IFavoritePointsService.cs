using FluentResults;
using GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;
using GeoEnjoy.Application.Contracts.Response;

namespace GeoEnjoy.Application.Services.PointsOfInterest;

public interface IFavoritePointsService
{
    Task<Result> AddAsync(Guid pointId);
    Task<Result> RemoveAsync(Guid pointId);

    Task<Result<List<PointOfInterestResponse>>> GetOwnAsync(GetOwnPointsOfInterestRequest request);
}
