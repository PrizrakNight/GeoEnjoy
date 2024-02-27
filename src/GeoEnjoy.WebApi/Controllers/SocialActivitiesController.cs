using FluentResults;
using GeoEnjoy.Application.Errors;
using GeoEnjoy.Application.Services.SocialActivities;
using GeoEnjoy.WebApi.Contracts;
using GeoEnjoy.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoEnjoy.WebApi.Controllers
{
    [Route("api/social-activities")]
    [Authorize]
    [ApiController]
    public class SocialActivitiesController(IServiceProvider serviceProvider)
        : ControllerBase
    {
        [HttpDelete("{entityType}/{entityId}")]
        public async Task<IActionResult> RemoveSocialActivities(EntityType entityType, Guid entityId)
        {
            var service = GetServiceForEntityType(entityType);

            if (service.IsFailed)
                return service.ToActionResult();

            var result = await service.Value.RemoveSocialActivityAsync(entityId);

            return result.ToActionResult();
        }

        [HttpPost("{entityType}/{entityId}/like")]
        public async Task<IActionResult> Like(EntityType entityType, Guid entityId)
        {
            var service = GetServiceForEntityType(entityType);

            if (service.IsFailed)
                return service.ToActionResult();

            var result = await service.Value.LikeAsync(entityId);

            return result.ToActionResult();
        }

        [HttpPost("{entityType}/{entityId}/dislike")]
        public async Task<IActionResult> Dislike(EntityType entityType, Guid entityId)
        {
            var service = GetServiceForEntityType(entityType);

            if (service.IsFailed)
                return service.ToActionResult();

            var result = await service.Value.DislikeAsync(entityId);

            return result.ToActionResult();
        }

        private Result<ISocialActivitiesService> GetServiceForEntityType(EntityType entityType)
        {
            var service = serviceProvider.GetKeyedService<ISocialActivitiesService>(entityType);

            if (service == null)
                return Result.Fail(GeoEnjoyErrors.InvalidEntityType(entityType.ToString()));

            return Result.Ok(service);
        }
    }
}
