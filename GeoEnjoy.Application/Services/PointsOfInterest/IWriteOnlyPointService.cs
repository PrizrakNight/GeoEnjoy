using FluentResults;
using GeoEnjoy.Application.Contracts.Request;
using GeoEnjoy.Application.Contracts.Response;

namespace GeoEnjoy.Application.Services.PointsOfInterest
{
    public interface IWriteOnlyPointService
    {
        Task<Result<PointOfInterestResponse>> AddAsync(CreatePointOfInterestRequest request);

        Task<Result<PointOfInterestResponse>> SetPublicAsync(Guid id, bool isPublic);

        Task<Result> LikeAsync(Guid id);
        Task<Result> DislikeAsync(Guid id);
        Task<Result> DeleteAsync(Guid id);
    }
}
