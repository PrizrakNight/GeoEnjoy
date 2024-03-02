using GeoEnjoy.Domain;

namespace GeoEnjoy.Application.Services.SocialActivities
{
    public interface ISocialActivitiesServiceFactory
    {
        ISocialActivitiesService Create(SocialEntityType entityType);
    }
}
