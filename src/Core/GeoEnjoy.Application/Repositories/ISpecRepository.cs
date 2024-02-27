using GeoEnjoy.Application.Dto;
using NSpecifications;

namespace GeoEnjoy.Application.Repositories;

public interface ISpecRepository<TEntity>
{
    ValueTask<bool> ExistsBySpecAsync(ASpec<TEntity> spec,
        CancellationToken cancellationToken = default);

    ValueTask<List<TEntity>> FindAllBySpecAsync(ASpec<TEntity> spec,
        PaginationDto? pagination = null,
        CancellationToken cancellationToken = default);

    ValueTask<TEntity?> FindOneBySpecAsync(ASpec<TEntity> spec,
        CancellationToken cancellationToken = default);
}
