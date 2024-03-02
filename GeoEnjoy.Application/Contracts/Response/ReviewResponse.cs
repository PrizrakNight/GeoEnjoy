namespace GeoEnjoy.Application.Contracts.Response;

public class ReviewResponse
{
    public Guid Id { get; set; }

    public string? Comment { get; set; }

    public short Rating { get; set; }

    /// <summary>
    /// It will be true if the current authorized user has put a like
    /// </summary>
    public bool HasLike { get; set; }

    /// <summary>
    /// It will be true if the current authorized user has put a dislike
    /// </summary>
    public bool HasDislike { get; set; }

    public long Likes { get; set; }
    public long Dislikes { get; set; }

    public DateTime PublicatedAt { get; set; }

    public GeoUserResponse? Reviewer { get; set; }
}
