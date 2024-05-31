using FluentResults;
using GeoEnjoy.Application.Contracts.Requests;
using GeoEnjoy.Application.Contracts.Response;

namespace GeoEnjoy.Application.Services.Reviews;

public interface IReviewService
{
    Task<Result<ReviewResponse>> AddAsync(Guid pointId, AddReviewRequest request);

    Task<Result> DeleteAsync(Guid id);

    Task<Result<List<ReviewResponse>>> GetAllAsync(Guid pointId, GetReviewsRequest request);

    Task<Result<ReviewResponse?>> GetOwnReviewAsync(Guid pointId);
}
