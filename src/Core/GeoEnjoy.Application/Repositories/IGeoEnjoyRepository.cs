using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Repositories;

public interface IGeoEnjoyRepository
{
    IRepository<Review> Reviews { get; }

    IFavoritePointOfInterestRepository FavoritePointsOfInterest { get; }
    IPointOfInterestRepository PointsOfInterest { get; }

    IRepository<T> Set<T>() where T : IDomainEntity;

    ValueTask SaveChangesAsync(CancellationToken cancellationToken = default);
}
