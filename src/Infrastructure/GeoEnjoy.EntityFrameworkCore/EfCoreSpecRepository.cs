using GeoEnjoy.Application.Dto;
using GeoEnjoy.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using NSpecifications;

namespace GeoEnjoy.EntityFrameworkCore;

public class EfCoreSpecRepository<T>(DbContext context,
    IEntityExpands<T> expands,
    IEntitySortings<T> sortings) : ISpecRepository<T>
    where T : class
{
    protected readonly DbSet<T> DbSet = context.Set<T>();

    public ValueTask<bool> ExistsBySpecAsync(ASpec<T> spec,
        CancellationToken cancellationToken = default)
    {
        var existsTask = DbSet.AnyAsync(spec, cancellationToken);

        return new ValueTask<bool>(existsTask);
    }

    public ValueTask<List<T>> FindAllBySpecAsync(ASpec<T> spec,
        PaginationDto? pagination = null,
        CancellationToken cancellationToken = default)
    {
        var query = ApplySortings(ExpandEntity(DbSet.AsNoTracking()));

        query = ApplyPagination(query.Where(spec), pagination);

        return new ValueTask<List<T>>(query.ToListAsync(cancellationToken));
    }

    public ValueTask<T?> FindOneBySpecAsync(ASpec<T> spec,
        CancellationToken cancellationToken = default)
    {
        var findTask = DbSet.FirstOrDefaultAsync(spec, cancellationToken);

        return new ValueTask<T?>(findTask);
    }

    protected IQueryable<T> ExpandEntity(IQueryable<T> query)
    {
        foreach (var propertyToExpand in expands.PropertiesToExpands)
        {
            query = query.Include(propertyToExpand);
        }

        return query;
    }

    protected IQueryable<T> ApplyPagination(IQueryable<T> query, PaginationDto? pagination)
    {
        if (pagination == null)
            return query;

        return query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize);
    }

    protected IQueryable<T> ApplySortings(IQueryable<T> query)
    {
        if (sortings.SortingBy == null)
            return query;

        var orderedQuery = sortings.SortingBy.Descending
            ? query.OrderByDescending(sortings.SortingBy.PropertySelector)
            : query.OrderBy(sortings.SortingBy.PropertySelector);

        if (sortings.SortingThenBy != null)
        {
            orderedQuery = sortings.SortingThenBy.Descending
                ? orderedQuery.ThenByDescending(sortings.SortingThenBy.PropertySelector)
                : orderedQuery.ThenBy(sortings.SortingThenBy.PropertySelector);
        }

        return orderedQuery;
    }
}
