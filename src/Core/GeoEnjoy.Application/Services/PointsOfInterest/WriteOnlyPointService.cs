using FluentResults;
using GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Errors;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain.Entities.PointOfInterests;
using PointSpec = GeoEnjoy.Application.Specifications.PointOfInterestSpecifications;

namespace GeoEnjoy.Application.Services.PointsOfInterest;

public class WriteOnlyPointService(
    IGeoEnjoyRepository repository,
    ICurrentUserProvider currentUser,
    ICancellationTokenProvider tokenProvider,
    IMappingService mapping) : IWriteOnlyPointService
{
    public async Task<Result<PointOfInterestResponse>> AddAsync(CreatePointOfInterestRequest request)
    {
        var pointOfInterest = mapping.Map<PointOfInterest>(request);

        pointOfInterest.AuthorId = currentUser.Id;

        repository.PointsOfInterest.Add(pointOfInterest);

        await repository.SaveChangesAsync(tokenProvider.CancellationToken);

        var response = mapping.Map<PointOfInterestResponse>(pointOfInterest);

        return Result.Ok(response);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var condition = PointSpec.ByAuthor(currentUser.Id) & PointSpec.ById(id);

        var foundPoint = await repository.PointsOfInterest.FindOneBySpecAsync
        (
            spec: condition,
            cancellationToken: tokenProvider.CancellationToken
        );

        if (foundPoint == null)
        {
            return Result.Fail(GeoEnjoyErrors.EntityNotFound<PointOfInterest>());
        }

        repository.PointsOfInterest.Delete(foundPoint);

        await repository.SaveChangesAsync(tokenProvider.CancellationToken);

        return Result.Ok();
    }

    public async Task<Result<PointOfInterestResponse>> SetPublicAsync(Guid id, bool isPublic)
    {
        var condition = PointSpec.ByAuthor(currentUser.Id) & PointSpec.ById(id);

        var foundPoint = await repository.PointsOfInterest.FindOneBySpecAsync
        (
            spec: condition,
            cancellationToken: tokenProvider.CancellationToken
        );

        if (foundPoint == null)
        {
            return Result.Fail(GeoEnjoyErrors.EntityNotFound<PointOfInterest>());
        }

        foundPoint.IsPublic = isPublic;

        repository.PointsOfInterest.Update(foundPoint);

        await repository.SaveChangesAsync(tokenProvider.CancellationToken);

        var response = mapping.Map<PointOfInterestResponse>(foundPoint);

        return Result.Ok(response);
    }
}
