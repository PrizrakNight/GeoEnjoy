using GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Services.PointsOfInterest;
using GeoEnjoy.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoEnjoy.WebApi.Controllers
{
    [Route("api/favorite-points")]
    [ApiController]
    [Authorize]
    public class FavoritePointsController(
        IFavoritePointsService favoritePoints
        ) : ControllerBase
    {
        [HttpPost("{pointId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(Guid pointId)
        {
            var result = await favoritePoints.AddAsync(pointId);

            return result.ToActionResult();
        }

        [HttpDelete("{pointId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid pointId)
        {
            var result = await favoritePoints.RemoveAsync(pointId);

            return result.ToActionResult();
        }

        [HttpPost("list")]
        [ProducesResponseType(typeof(List<PointOfInterestResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetList([FromBody] GetOwnPointsOfInterestRequest request)
        {
            var result = await favoritePoints.GetAsync(request);

            return result.ToActionResult();
        }
    }
}
