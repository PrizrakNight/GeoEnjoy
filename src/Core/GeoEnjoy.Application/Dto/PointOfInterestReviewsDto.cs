using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Dto;

public class PointOfInterestReviewsDto
{
    public Review? ReviewByAuthor { get; set; }

    public ICollection<Review> TopRelevantReviews { get; set; } = new HashSet<Review>();
}
