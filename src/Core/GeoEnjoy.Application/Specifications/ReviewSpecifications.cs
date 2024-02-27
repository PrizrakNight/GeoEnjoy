using GeoEnjoy.Domain.Entities;
using NSpecifications;

namespace GeoEnjoy.Application.Specifications;

public static class ReviewSpecifications
{
    public static Spec<Review> ByPointOfInterest(Guid pointId)
    {
        throw new NotImplementedException();
        //return new Spec<Review>(x => x.PointOfInterestId == pointId);
    }

    public static Spec<Review> ByReviewer(Guid reviewerId)
    {
        throw new NotImplementedException();
        //return new Spec<Review>(x => x.ReviewerId == reviewerId);
    }

    public static Spec<Review> ById(Guid reviewId)
    {
        return new Spec<Review>(x => x.Id == reviewId);
    }
}
