namespace GeoEnjoy.Domain.Entities.PointOfInterests;

public class PointOfInterestAssessment
{
    public Guid PointId { get; set; }

    public float Rating { get; set; }

    public long Likes { get; set; }
    public long Dislikes { get; set; }
}
