using GeoEnjoy.Application.Dto;

namespace GeoEnjoy.Application.Contracts.Requests.PointsOfInterest;

public class CreatePointOfInterestRequest
{
    public bool IsPublic { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public PointDto Point { get; set; } = null!;
}
