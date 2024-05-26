using GeoEnjoy.Domain.Entities.PointOfInterests;

namespace GeoEnjoy.Domain.Entities;

public enum SocialActivityType
{
    Like,
    Dislike
}

public class SocialActivity : ICreatable, IModifiable
{
    /// <summary>
    /// The ID of the user who likes
    /// </summary>
    public Guid UserId { get; set; }

    public SocialActivityType ActivityType { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual Review? Review { get; set; }
    public virtual PointOfInterest? PointOfInterest { get; set; }
}
