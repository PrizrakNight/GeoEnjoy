using FluentResults;
using GeoEnjoy.Application.Contracts.Request;
using GeoEnjoy.Application.Contracts.Response;

namespace GeoEnjoy.Application.Services.PointsOfInterest
{
    public interface IFavoritePointsService
    {
        Task<Result> AddAsync(Guid pointId);
        Task<Result> RemoveAsync(Guid pointId);

        Task<Result<List<PointOfInterestResponse>>> GetAsync(GetOwnPointsOfInterestRequest request);
    }
}
