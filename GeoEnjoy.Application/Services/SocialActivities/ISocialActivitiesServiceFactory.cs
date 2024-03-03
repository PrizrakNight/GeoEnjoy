using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Services.SocialActivities
{
    public interface ISocialActivitiesServiceFactory
    {
        ISocialActivitiesService Create(SocialEntityType entityType);
    }
}
