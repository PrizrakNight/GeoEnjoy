using GeoEnjoy.Application.Contracts.Request;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Dto;
using GeoEnjoy.Application.Services.PointsOfInterest;
using GeoEnjoy.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GeoEnjoy.WebApi.Controllers
{
    [Route("api/points-of-interest")]
    [ApiController]
    [AutoValidateAntiforgeryToken]
    public class PointsOfInterestController(
        IReadOnlyPointService readOnlyPoints) : ControllerBase
    {
        [HttpPost("in-radius")]
        [ProducesResponseType(typeof(List<PointOfInterestResponse>), 200)]
        public async Task<IActionResult> GetInRadiusAsync([FromBody] RadiusDto request)
        {
            var result = await readOnlyPoints.GetInRadiusAsync(request);

            return result.ToActionResult();
        }

        [HttpPost("own")]
        [ProducesResponseType(typeof(List<PointOfInterestResponse>), 200)]
        public async Task<IActionResult> GetInRadiusAsync([FromBody] GetOwnPointsOfInterestRequest request)
        {
            var result = await readOnlyPoints.GetOwnAsync(request);

            return result.ToActionResult();
        }
    }
}
