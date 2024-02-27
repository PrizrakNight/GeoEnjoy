namespace GeoEnjoy.Application.Dto;

public class LikesDislikesDto
{
    public bool UserHashLike { get; set; }
    public bool UserHasDislike { get; set; }

    public long Likes { get; set; }
    public long Dislikes { get; set; }
}
