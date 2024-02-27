using GeoEnjoy.Application.Dto;
using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Contracts.Response;

public class PointOfInterestResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public SocialActivityType? OwnSocialActivity { get; set; }

    /// <summary>
    /// Point of interest rating from 0.0 to 5.0
    /// </summary>
    public float Rating { get; set; }

    public long Likes { get; set; }
    public long Dislikes { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public PointDto Point { get; set; } = null!;
}
