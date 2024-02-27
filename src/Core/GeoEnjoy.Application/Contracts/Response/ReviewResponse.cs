using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Contracts.Response;

public class ReviewResponse
{
    public Guid Id { get; set; }

    public string? Comment { get; set; }

    public short Rating { get; set; }

    public SocialActivityType? OwnSocialActivity { get; set; }

    public long Likes { get; set; }
    public long Dislikes { get; set; }

    public DateTime PublicatedAt { get; set; }

    public GeoUserResponse? Reviewer { get; set; }
}
