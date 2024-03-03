namespace GeoEnjoy.Domain.Entities;

public class Review : IDomainEntity
{
    public Guid Id { get; set; }
    public Guid PointOfInterestId { get; set; }
    public Guid ReviewerId { get; set; }

    public string? Comment { get; set; }

    /// <summary>
    /// A rating from the reviewer (From 1 to 5)
    /// </summary>
    public short Rating { get; set; }

    public DateTime Created { get; set; }

    public long Likes { get; set; }
    public long Dislikes { get; set; }
}
