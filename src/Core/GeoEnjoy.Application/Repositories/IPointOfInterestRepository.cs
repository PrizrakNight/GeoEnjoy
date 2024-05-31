using GeoEnjoy.Application.Dto;
using GeoEnjoy.Domain.Entities.PointOfInterests;
using NSpecifications;

namespace GeoEnjoy.Application.Repositories;

public interface IPointOfInterestRepository : IRepository<PointOfInterest>
{
    ValueTask<List<PointOfInterest>> GetInRadiusAsync(RadiusDto radius,
        ASpec<PointOfInterest>? spec = null,
        CancellationToken cancellationToken = default);

    ValueTask<List<PointOfInterestAssessment>> GetPointOfInterestsAssessmentsAsync(IEnumerable<Guid> pointIds,
        CancellationToken cancellationToken = default);
}
