﻿using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Sortings;

public static class FavoritePointSorting
{
    public static readonly List<Sorting> Newer =
    [
        new(nameof(FavoritePointOfInterest.Created), true)
    ];

    public static readonly List<Sorting> Older =
    [
        new(nameof(FavoritePointOfInterest.Created), false)
    ];
}
