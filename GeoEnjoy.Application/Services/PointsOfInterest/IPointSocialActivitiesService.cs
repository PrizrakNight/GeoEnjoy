using FluentResults;

namespace GeoEnjoy.Application.Services.PointsOfInterest
{
    public interface IPointSocialActivitiesService
    {
        Task<Result> RemoveSocialActivitiesAsync(Guid id);
        Task<Result> LikeAsync(Guid id);
        Task<Result> DislikeAsync(Guid id);
    }
}
