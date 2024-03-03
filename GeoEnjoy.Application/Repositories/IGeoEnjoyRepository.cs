using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Repositories;

public interface IGeoEnjoyRepository
{
    IRepository<Review> Reviews { get; }

    IUserSocialActivityRepository UserSocialActivities { get; }
    IFavoritePointOfInterestRepository FavoritePointsOfInterest { get; }
    IPointOfInterestRepository PointsOfInterest { get; }

    ValueTask SaveChangesAsync(CancellationToken cancellationToken = default);
}
