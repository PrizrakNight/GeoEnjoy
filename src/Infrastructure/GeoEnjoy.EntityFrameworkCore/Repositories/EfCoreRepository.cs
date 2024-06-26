﻿using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoEnjoy.EntityFrameworkCore.Repositories;

public class EfCoreRepository<T>(DbContext context,
    IEntityExpands<T> expands,
    IEntitySortings<T> sortings) : EfCoreSpecRepository<T>(context, expands, sortings), IRepository<T>
    where T : class, IDomainEntity
{
    public T Add(T entity)
    {
        DbSet.Add(entity);

        return entity;
    }

    public void Delete(T entity)
    {
        DbSet.Remove(entity);
    }

    public ValueTask<bool> ExistsByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var existsTask = DbSet
            .AnyAsync(x => x.Id == id, cancellationToken);

        return new ValueTask<bool>(existsTask);
    }

    public ValueTask<T?> FindByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        var findTask = ExpandEntity(DbSet)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return new ValueTask<T?>(findTask);
    }

    public ValueTask<List<T>> FindByIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        var findTask = ApplySortings(ExpandEntity(DbSet.AsNoTracking()))
            .ToListAsync(cancellationToken);

        return new ValueTask<List<T>>(findTask);
    }

    public T Update(T entity)
    {
        // We don't do anything because EF will track the changes itself

        return entity;
    }
}
