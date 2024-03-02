using GeoEnjoy.Application.Contracts.Requests;
using GeoEnjoy.Application.Services.SocialActivities;
using GeoEnjoy.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoEnjoy.WebApi.Controllers
{
    [Route("api/social-activities")]
    [Authorize]
    [ApiController]
    public class SocialActivitiesController(
        ISocialActivitiesServiceFactory factory) : ControllerBase
    {
        [HttpDelete]
        public async Task<IActionResult> RemoveSocialActivities([FromBody] SocialActivityRequest request)
        {
            var service = factory.Create(request.EntityType);

            var result = await service.RemoveSocialActivitiesAsync(request.EntityId);

            return result.ToActionResult();
        }

        [HttpPost("like")]
        public async Task<IActionResult> Like([FromBody] SocialActivityRequest request)
        {
            var service = factory.Create(request.EntityType);

            var result = await service.LikeAsync(request.EntityId);

            return result.ToActionResult();
        }

        [HttpPost("dislike")]
        public async Task<IActionResult> Dislike([FromBody] SocialActivityRequest request)
        {
            var service = factory.Create(request.EntityType);

            var result = await service.DislikeAsync(request.EntityId);

            return result.ToActionResult();
        }
    }
}
