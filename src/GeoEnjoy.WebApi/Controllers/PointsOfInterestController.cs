using GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Dto;
using GeoEnjoy.Application.Services.PointsOfInterest;
using GeoEnjoy.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoEnjoy.WebApi.Controllers;

[Route("api/points-of-interest")]
[ApiController]
[Authorize]
[AutoValidateAntiforgeryToken]
public class PointsOfInterestController(
    IReadOnlyPointService readOnlyPoints,
    IWriteOnlyPointService writeOnlyPoints) : ControllerBase
{
    [HttpPost("{pointId}/set-public")]
    [ProducesResponseType(typeof(List<PointOfInterestResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetPublic(Guid pointId,
        [FromBody] SetPublicRequest request)
    {
        var result = await writeOnlyPoints.SetPublicAsync
        (
            id: pointId,
            isPublic: request.IsPublic
        );

        return result.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<PointOfInterestResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(
        [FromBody] CreatePointOfInterestRequest request)
    {
        var result = await writeOnlyPoints.AddAsync(request);

        return result.ToActionResult();
    }

    [HttpDelete("{pointId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid pointId)
    {
        var result = await writeOnlyPoints.DeleteAsync(pointId);

        return result.ToActionResult();
    }

    #region ReadOnly

    [HttpPost("in-radius")]
    [ProducesResponseType(typeof(List<PointOfInterestResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInRadius([FromBody] RadiusDto request)
    {
        var result = await readOnlyPoints.GetInRadiusAsync(request);

        return result.ToActionResult();
    }

    [HttpPost("own")]
    [ProducesResponseType(typeof(List<PointOfInterestResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOwn([FromBody] GetOwnPointsOfInterestRequest request)
    {
        var result = await readOnlyPoints.GetOwnAsync(request);

        return result.ToActionResult();
    }

    #endregion
}
