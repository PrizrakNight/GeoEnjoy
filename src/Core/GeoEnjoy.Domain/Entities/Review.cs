using GeoEnjoy.Domain.Entities.PointOfInterests;

namespace GeoEnjoy.Domain.Entities;

public class Review : ISocialActivityEntity
{
    public Guid Id { get; set; }

    /// <summary>
    /// ID of the user who created the review
    /// </summary>
    public Guid ReviewerId { get; set; }

    public string? Comment { get; set; }

    /// <summary>
    /// A rating from the reviewer (From 1 to 5)
    /// </summary>
    public short Rating { get; set; }

    public DateTime Created { get; set; }

    public virtual PointOfInterest? PointOfInterest { get; set; }

    public virtual ICollection<SocialActivity>? SocialActivities { get; set; }
}
