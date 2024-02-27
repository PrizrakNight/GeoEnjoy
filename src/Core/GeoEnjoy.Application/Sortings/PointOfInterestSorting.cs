using GeoEnjoy.Domain.Entities.PointOfInterests;

namespace GeoEnjoy.Application.Sortings;

[Obsolete($"Use IEntitySortings instead of this")]
public static class PointOfInterestSorting
{
    public static readonly List<Sorting> Newer =
    [
        new(nameof(PointOfInterest.Created), true)
    ];

    public static readonly List<Sorting> Older =
    [
        new(nameof(PointOfInterest.Created), false)
    ];
}
