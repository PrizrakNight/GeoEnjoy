namespace GeoEnjoy.Domain.Entities.PointOfInterests;

public class PointOfInterest : ISocialActivityEntity
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }

    public bool IsPublic { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public double Longitude { get; set; }
    public double Latitude { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<SocialActivity>? SocialActivities { get; set; }
}
