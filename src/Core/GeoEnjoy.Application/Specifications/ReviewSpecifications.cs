using GeoEnjoy.Domain.Entities;
using NSpecifications;

namespace GeoEnjoy.Application.Specifications;

public static class ReviewSpecifications
{
    public static Spec<Review> ByPointOfInterest(Guid pointId)
    {
        return new Spec<Review>(x => x.PointOfInterest!.Id == pointId);
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
