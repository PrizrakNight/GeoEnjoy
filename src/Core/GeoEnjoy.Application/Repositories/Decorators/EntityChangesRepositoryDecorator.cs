using GeoEnjoy.Application.Dto;
using GeoEnjoy.Domain.Entities;
using NSpecifications;

namespace GeoEnjoy.Application.Repositories.Decorators;

public class EntityChangesRepositoryDecorator<T>(IRepository<T> decorated) : IRepository<T>
    where T : IDomainEntity
{
    public T Add(T entity)
    {
        if (entity is ICreatable creatable)
            creatable.Created = DateTime.UtcNow;

        decorated.Add(entity);

        return entity;
    }

    public T Update(T entity)
    {
        if (entity is IModifiable modifiable)
            modifiable.Updated = DateTime.UtcNow;

        decorated.Update(entity);

        return entity;
    }

    public void Delete(T entity)
    {
        decorated.Delete(entity);
    }

    public ValueTask<bool> ExistsByIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        return decorated.ExistsByIdAsync(id, cancellationToken);
    }

    public ValueTask<bool> ExistsBySpecAsync(ASpec<T> spec,
        CancellationToken cancellationToken = default)
    {
        return decorated.ExistsBySpecAsync(spec, cancellationToken);
    }

    public ValueTask<List<T>> FindAllBySpecAsync(ASpec<T> spec,
        PaginationDto? pagination = null,
        CancellationToken cancellationToken = default)
    {
        return decorated.FindAllBySpecAsync(spec, pagination, cancellationToken);
    }

    public ValueTask<T?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return decorated.FindByIdAsync(id, cancellationToken);
    }

    public ValueTask<List<T>> FindByIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        return decorated.FindByIdsAsync(ids, cancellationToken);
    }

    public ValueTask<T?> FindOneBySpecAsync(ASpec<T> spec,
        CancellationToken cancellationToken = default)
    {
        return decorated.FindOneBySpecAsync(spec, cancellationToken);
    }
}
