using GeoEnjoy.Domain.Entities;
using NSpecifications;

namespace GeoEnjoy.Application.Specifications;

public static class FavoritePointOfInterestSpecifications
{
    public static Spec<FavoritePointOfInterest> ByUser(Guid userId)
    {
        return new Spec<FavoritePointOfInterest>(x => x.UserId == userId);
    }

    public static Spec<FavoritePointOfInterest> ByPoint(Guid pointId)
    {
        return new Spec<FavoritePointOfInterest>(x => x.PointId == pointId);
    }
}
