using GeoEnjoy.Domain.Entities.PointOfInterests;
using NSpecifications;

namespace GeoEnjoy.Application.Specifications;

public static class PointOfInterestSpecifications
{
    public static Spec<PointOfInterest> ByAuthor(Guid authorId)
    {
        return new Spec<PointOfInterest>(x => x.AuthorId == authorId);
    }

    public static Spec<PointOfInterest> ById(Guid pointId)
    {
        return new Spec<PointOfInterest>(x => x.Id == pointId);
    }

    public static Spec<PointOfInterest> OnlyPublic()
    {
        return new Spec<PointOfInterest>(x => x.IsPublic == true);
    }
}
