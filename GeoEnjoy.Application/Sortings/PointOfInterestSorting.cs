using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Sortings;

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
