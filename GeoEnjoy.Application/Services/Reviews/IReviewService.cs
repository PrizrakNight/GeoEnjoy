﻿using FluentResults;
using GeoEnjoy.Application.Contracts.Request;
using GeoEnjoy.Application.Contracts.Response;

namespace GeoEnjoy.Application.Services.Reviews;

public interface IReviewService
{
    Task<Result<ReviewResponse>> AddAsync(Guid pointId, AddReviewRequest request);

    Task<Result<List<ReviewResponse>>> GetAsync(Guid pointId, GetReviewsRequest request);

    Task<Result<ReviewResponse?>> GetOwnReviewAsync(Guid pointId);

    Task<Result> DeleteAsync(Guid id);
}
