namespace GeoEnjoy.Domain.Entities;

public class FavoritePointOfInterest
{
    public Guid PointId { get; set; }
    public Guid UserId { get; set; }

    public DateTime Created { get; set; }
}
