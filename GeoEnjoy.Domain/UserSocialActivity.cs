namespace GeoEnjoy.Domain;

public enum SocialActivityType
{
    Like,
    Dislike
}

public enum SocialEntityType
{
    PointOfInterest,
    Review
}

public class UserSocialActivity : IDomainEntity
{
    /// <summary>
    /// The ID of the entity that is being liked
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The ID of the user who likes
    /// </summary>
    public Guid UserId { get; set; }

    public SocialActivityType ActivityType { get; set; }
    public SocialEntityType EntityType { get; set; }

    public DateTime Created { get; set; }
}
