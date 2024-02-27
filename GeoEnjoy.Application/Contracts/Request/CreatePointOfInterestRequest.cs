using GeoEnjoy.Application.Dto;

namespace GeoEnjoy.Application.Contracts.Request
{
    public class CreatePointOfInterestRequest
    {
        public bool IsPublic { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public PointDto Point { get; set; } = null!;
    }
}
