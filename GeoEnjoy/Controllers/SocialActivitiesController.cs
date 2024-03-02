using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Services.PointsOfInterest;
using GeoEnjoy.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoEnjoy.WebApi.Controllers
{
    [Route("api/social-activities")]
    [Authorize]
    [ApiController]
    public class SocialActivitiesController(
        IPointSocialActivitiesService pointSocialActivities) : ControllerBase
    {
        [HttpDelete("points/{pointId}")]
        [ProducesResponseType(typeof(List<PointOfInterestResponse>), 200)]
        public async Task<IActionResult> RemoveSocialActivities(Guid pointId)
        {
            var result = await pointSocialActivities.RemoveSocialActivitiesAsync(pointId);

            return result.ToActionResult();
        }

        [HttpPost("points/{pointId}/like")]
        [ProducesResponseType(typeof(List<PointOfInterestResponse>), 200)]
        public async Task<IActionResult> Like(Guid pointId)
        {
            var result = await pointSocialActivities.LikeAsync(pointId);

            return result.ToActionResult();
        }

        [HttpPost("points/{pointId}/dislike")]
        [ProducesResponseType(typeof(List<PointOfInterestResponse>), 200)]
        public async Task<IActionResult> Dislike(Guid pointId)
        {
            var result = await pointSocialActivities.DislikeAsync(pointId);

            return result.ToActionResult();
        }
    }
}
