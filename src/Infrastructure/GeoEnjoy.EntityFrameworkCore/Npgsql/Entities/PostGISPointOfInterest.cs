using GeoEnjoy.Domain.Entities.PointOfInterests;
using NetTopologySuite.Geometries;

namespace GeoEnjoy.EntityFrameworkCore.Npgsql.Entities;

public class PostGISPointOfInterest : PointOfInterest
{
    public Point PointOnMap { get; set; } = null!;

    public override double Longitude
    {
        get => PointOnMap.Coordinate.X;
        set => PointOnMap.Coordinate.X = value;
    }

    public override double Latitude
    {
        get => PointOnMap.Coordinate.Y;
        set => PointOnMap.Coordinate.Y = value;
    }
}
