using FluentResults;

namespace GeoEnjoy.Application.Services.SocialActivities
{
    public interface ISocialActivitiesService
    {
        Task<Result> RemoveSocialActivityAsync(Guid id);
        Task<Result> LikeAsync(Guid id);
        Task<Result> DislikeAsync(Guid id);
    }
}
