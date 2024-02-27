using FluentResults;
using GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Dto;

namespace GeoEnjoy.Application.Services.PointsOfInterest;

public interface IReadOnlyPointService
{
    /// <summary>
    /// Retrieves all points of interest of the current authorized user
    /// </summary>
    Task<Result<List<PointOfInterestResponse>>> GetOwnAsync(GetOwnPointsOfInterestRequest request);

    /// <summary>
    /// Gets all public points of interest within a given radius
    /// <para>
    /// It also returns non-public points if their author is the current authorized user
    /// </para>
    /// </summary>
    Task<Result<List<PointOfInterestResponse>>> GetInRadiusAsync(RadiusDto radius);
}
