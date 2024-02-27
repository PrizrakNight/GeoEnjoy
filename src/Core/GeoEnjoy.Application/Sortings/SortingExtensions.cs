using System.Linq.Expressions;

namespace GeoEnjoy.Application.Sortings;

public static class SortingExtensions
{
    [Obsolete($"Use IEntitySortings instead of this")]
    public static IOrderedQueryable<T> ApplyTo<T>(this IEnumerable<Sorting> sortings,
            IQueryable<T> query)
    {
        ArgumentNullException.ThrowIfNull(nameof(sortings));
        ArgumentNullException.ThrowIfNull(nameof(query));

        IOrderedQueryable<T> orderedQueryable = null!;

        foreach (var sorting in sortings)
        {
            if (orderedQueryable != null)
            {
                if (sorting.Descending)
                {
                    query = orderedQueryable
                        .ThenByDescending(CreateKeySelector<T>(sorting));
                }
                else
                {
                    query = orderedQueryable
                        .ThenBy(CreateKeySelector<T>(sorting));
                }
            }
            else
            {
                if (sorting.Descending)
                {
                    orderedQueryable = query
                        .OrderByDescending(CreateKeySelector<T>(sorting));
                }
                else
                {
                    orderedQueryable = query
                        .OrderBy(CreateKeySelector<T>(sorting));
                }
            }
        }

        return orderedQueryable;
    }

    [Obsolete($"Use IEntitySortings instead of this")]
    public static IOrderedEnumerable<T> ApplyTo<T>(this IEnumerable<Sorting> sortings,
            IEnumerable<T> query)
    {
        ArgumentNullException.ThrowIfNull(nameof(sortings));
        ArgumentNullException.ThrowIfNull(nameof(query));

        IOrderedEnumerable<T> orderedEnumerable = null!;

        foreach (var sorting in sortings)
        {
            if (orderedEnumerable != null)
            {
                if (sorting.Descending)
                {
                    query = orderedEnumerable
                        .ThenByDescending(CreateKeySelector<T>(sorting).Compile());
                }
                else
                {
                    query = orderedEnumerable
                        .ThenBy(CreateKeySelector<T>(sorting).Compile());
                }
            }
            else
            {
                if (sorting.Descending)
                {
                    orderedEnumerable = query
                        .OrderByDescending(CreateKeySelector<T>(sorting).Compile());
                }
                else
                {
                    orderedEnumerable = query
                        .OrderBy(CreateKeySelector<T>(sorting).Compile());
                }
            }
        }

        return orderedEnumerable;
    }

    // TODO: It is possible to make a smarter selector generation taking into account the types of final properties
    /// <summary>
    /// Trivial implementation of the selector build
    /// <para>
    /// There is a conversion to the Object type, which results in boxing for value types
    /// </para>
    /// </summary>
    private static Expression<Func<T, object>> CreateKeySelector<T>(Sorting sorting)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var pathSegments = sorting.PropertyPath.Split(".");

        Expression lastExpression = parameter;

        foreach (var pathSegment in pathSegments)
        {
            lastExpression = Expression.Property(lastExpression, pathSegment);
        }

        lastExpression = Expression.Convert(lastExpression, typeof(object));

        var lambda = Expression.Lambda<Func<T, object>>(lastExpression, parameter);

        return lambda;
    }
}
