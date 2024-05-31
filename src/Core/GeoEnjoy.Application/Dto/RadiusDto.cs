namespace GeoEnjoy.Application.Dto;

public class RadiusDto
{
    public PointDto Point { get; set; } = null!;

    public double RadiusMeters { get; set; }
}
