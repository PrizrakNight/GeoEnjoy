using FluentResults;
using GeoEnjoy.Application.Contracts.Requests;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Errors;
using GeoEnjoy.Application.Extensions;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain.Entities;
using GeoEnjoy.Domain.Entities.PointOfInterests;
using ReviewSpec = GeoEnjoy.Application.Specifications.ReviewSpecifications;

namespace GeoEnjoy.Application.Services.Reviews;

public class ReviewService(
    IGeoEnjoyRepository repository,
    ICurrentUserProvider currentUser,
    ICancellationTokenProvider tokenProvider,
    IMappingService mapping,
    IEntitySortings<Review> reviewSortings) : IReviewService
{
    public async Task<Result<ReviewResponse>> AddAsync(Guid pointId, AddReviewRequest request)
    {
        var cancellationToken = tokenProvider.CancellationToken;

        var pointExists = await repository.PointsOfInterest.ExistsByIdAsync
        (
            id: pointId,
            cancellationToken: cancellationToken
        );

        if (!pointExists)
        {
            return Result.Fail<ReviewResponse>(GeoEnjoyErrors.EntityNotFound<PointOfInterest>());
        }

        var currrentUserId = currentUser.Id;

        var condition = ReviewSpec.ByReviewer(currrentUserId) & ReviewSpec.ByPointOfInterest(pointId);

        var foundReview = await repository.Reviews.FindOneBySpecAsync
        (
            spec: condition,
            cancellationToken: cancellationToken
        );

        if (foundReview != null)
        {
            repository.Reviews.Delete(foundReview);
        }

        var review = mapping.Map<Review>(request);

        review.ReviewerId = currrentUserId;
        review.PointOfInterest = new PointOfInterest { Id = pointId };

        repository.Reviews.Add(review);

        await repository.SaveChangesAsync(tokenProvider.CancellationToken);

        var response = mapping.Map<ReviewResponse>(review);

        return Result.Ok(response);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var currentUserId = currentUser.Id;

        var condition = ReviewSpec.ByReviewer(currentUserId) & ReviewSpec.ById(id);

        var foundReview = await repository.Reviews.FindOneBySpecAsync
        (
            spec: condition,
            cancellationToken: tokenProvider.CancellationToken
        );

        if (foundReview == null)
        {
            return Result.Fail(GeoEnjoyErrors.EntityNotFound<Review>());
        }

        repository.Reviews.Delete(foundReview);

        await repository.SaveChangesAsync(tokenProvider.CancellationToken);

        return Result.Ok();
    }

    public async Task<Result<List<ReviewResponse>>> GetAllAsync(Guid pointId, GetReviewsRequest request)
    {
        var pointNotExists = await repository.PointsOfInterest.ExistsByIdAsync(pointId) == false;

        if (pointNotExists)
        {
            return Result.Fail(GeoEnjoyErrors.EntityNotFound<PointOfInterest>());
        }

        var condition = ReviewSpec.ByPointOfInterest(pointId);

        reviewSortings.UseSortingWay(request.Sorting);

        var sortedReviews = await repository.Reviews.FindAllBySpecAsync
        (
            spec: condition,
            pagination: request.Pagination,
            cancellationToken: tokenProvider.CancellationToken
        );

        var response = mapping.Map<List<ReviewResponse>>(sortedReviews);

        return Result.Ok(response);
    }

    public async Task<Result<ReviewResponse?>> GetOwnReviewAsync(Guid pointId)
    {
        var condition = ReviewSpec.ByPointOfInterest(pointId) & ReviewSpec.ByReviewer(currentUser.Id);

        var foundReview = await repository.Reviews.FindOneBySpecAsync
        (
            spec: condition,
            cancellationToken: tokenProvider.CancellationToken
        );

        var response = foundReview != null
            ? mapping.Map<ReviewResponse>(foundReview)
            : null;

        return Result.Ok(response);
    }
}
