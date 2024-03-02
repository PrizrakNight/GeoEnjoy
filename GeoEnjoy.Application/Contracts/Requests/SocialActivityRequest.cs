using GeoEnjoy.Domain;

namespace GeoEnjoy.Application.Contracts.Requests
{
    public class SocialActivityRequest
    {
        public Guid EntityId { get; set; }

        public SocialEntityType EntityType { get; set; }
    }
}
