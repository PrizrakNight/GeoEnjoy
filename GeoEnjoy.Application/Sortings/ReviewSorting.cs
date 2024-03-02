using GeoEnjoy.Domain;

namespace GeoEnjoy.Application.Sortings;

public static class ReviewSorting
{
    public static readonly List<Sorting> Relevant =
    [
        new(nameof(Review.Likes), true),
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
