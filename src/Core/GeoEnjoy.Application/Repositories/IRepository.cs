using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Repositories;

public interface IRepository<TEntity> : ISpecRepository<TEntity>
    where TEntity : IDomainEntity
{
    ValueTask<bool> ExistsByIdAsync(Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<TEntity?> FindByIdAsync(Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<List<TEntity>> FindByIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default);

    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);

    void Delete(TEntity entity);
}
