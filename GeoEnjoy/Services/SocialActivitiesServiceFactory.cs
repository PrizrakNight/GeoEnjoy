using GeoEnjoy.Application.Services.PointsOfInterest;
using GeoEnjoy.Application.Services.Reviews;
using GeoEnjoy.Application.Services.SocialActivities;
using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.WebApi.Services
{
    public class SocialActivitiesServiceFactory : ISocialActivitiesServiceFactory
    {
        private readonly Dictionary<SocialEntityType, ISocialActivitiesService> _strategies;

        public SocialActivitiesServiceFactory(IServiceProvider serviceProvider)
        {
            _strategies = new()
            {
                {SocialEntityType.PointOfInterest, serviceProvider.GetRequiredService<PointSocialActivitiesService>()},
                {SocialEntityType.Review, serviceProvider.GetRequiredService<ReviewSocialActivitiesService>()}
            };
        }

        public ISocialActivitiesService Create(SocialEntityType entityType)
        {
            if (!_strategies.TryGetValue(entityType, out var stategy))
                throw new NotImplementedException($"The strategy for '{entityType}' has not been implemented");

            return stategy;
        }
    }
}
