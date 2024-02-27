using GeoEnjoy.Application.Contracts.Requests;
using GeoEnjoy.Application.Contracts.Response;
using GeoEnjoy.Application.Services.Reviews;
using GeoEnjoy.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeoEnjoy.WebApi.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    [Authorize]
    public class ReviewsController(
        IReviewService reviewService
        ) : ControllerBase
    {
        [HttpPost("points/{pointId}")]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(Guid pointId,
            [FromBody] AddReviewRequest request)
        {
            var result = await reviewService.AddAsync(pointId, request);

            return result.ToActionResult();
        }

        [HttpPost("points/{pointId}/list")]
        [ProducesResponseType(typeof(List<ReviewResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllReviews(Guid pointId,
            [FromBody] GetReviewsRequest request)
        {
            var result = await reviewService.GetAsync(pointId, request);

            return result.ToActionResult();
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid reviewId)
        {
            var result = await reviewService.DeleteAsync(reviewId);

            return result.ToActionResult();
        }

        [HttpGet("points/{pointId}/own")]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOwn(Guid pointId)
        {
            var result = await reviewService.GetOwnReviewAsync(pointId);

            return result.ToActionResult();
        }
    }
}
