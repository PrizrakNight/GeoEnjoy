using GeoEnjoy.Application.Dto;
using GeoEnjoy.Domain.Entities;

namespace GeoEnjoy.Application.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    ValueTask<PointOfInterestReviewsDto> GetForPointAsync(Guid pointId,
        Guid? authorId,
        CancellationToken cancellationToken = default);
}
