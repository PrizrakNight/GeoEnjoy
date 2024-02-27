using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Sortings;

[Obsolete($"Use IEntitySortings instead of this")]
public static class ReviewSorting
{
    public static readonly List<Sorting> Relevant =
    [
        //new(nameof(Review.Likes), true),
        new(nameof(Review.Created), true)
    ];

    public static readonly List<Sorting> Newer =
    [
        new(nameof(Review.Created), true)
    ];

    public static readonly List<Sorting> Older =
    [
        new(nameof(Review.Created), false)
    ];
}
