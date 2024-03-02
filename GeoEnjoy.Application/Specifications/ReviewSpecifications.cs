using GeoEnjoy.Domain;
using NSpecifications;

namespace GeoEnjoy.Application.Specifications;

public static class ReviewSpecifications
{
    public static Spec<Review> ByPointOfInterest(Guid pointId)
    {
        return new Spec<Review>(x => x.PointOfInterestId == pointId);
    }

    public static Spec<Review> ByReviewer(Guid reviewerId)
    {
        return new Spec<Review>(x => x.ReviewerId == reviewerId);
    }

    public static Spec<Review> ById(Guid reviewId)
    {
        return new Spec<Review>(x => x.Id == reviewId);
    }
}
