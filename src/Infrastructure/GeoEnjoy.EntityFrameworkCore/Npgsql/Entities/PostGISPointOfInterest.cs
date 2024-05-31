using GeoEnjoy.Domain.Entities.PointOfInterests;
using NetTopologySuite.Geometries;

namespace GeoEnjoy.EntityFrameworkCore.Npgsql.Entities;

public class PostGISPointOfInterest : PointOfInterest
{
    public Point PointOnMap { get; set; } = null!;

    public override double Longitude
    {
        get => PointOnMap.X;
        set => PointOnMap.X = value;
    }

    public override double Latitude
    {
        get => PointOnMap.Y;
        set => PointOnMap.Y = value;
    }
}
