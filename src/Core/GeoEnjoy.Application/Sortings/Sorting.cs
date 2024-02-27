namespace GeoEnjoy.Application.Sortings;

[Obsolete($"Use EntitySorting instead of this")]
public record Sorting(string PropertyPath, bool Descending);
