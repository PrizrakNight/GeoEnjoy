using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Repositories;

public interface IFavoritePointOfInterestRepository
    : ISpecRepository<FavoritePointOfInterest>
{
    void Add(FavoritePointOfInterest pointOfInterest);

    void DeleteBy(Guid userId, Guid pointId);
}
