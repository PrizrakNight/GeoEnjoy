using FluentResults;
using GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;
using GeoEnjoy.Application.Contracts.Response;

namespace GeoEnjoy.Application.Services.PointsOfInterest;

public interface IWriteOnlyPointService
{
    Task<Result<PointOfInterestResponse>> AddAsync(CreatePointOfInterestRequest request);
    Task<Result<PointOfInterestResponse>> SetPublicAsync(Guid id, bool isPublic);

    
    Task<Result> DeleteAsync(Guid id);
}
