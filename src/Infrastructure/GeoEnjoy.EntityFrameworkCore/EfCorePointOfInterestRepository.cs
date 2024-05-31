using GeoEnjoy.Application.Dto;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain.Entities.PointOfInterests;
using NSpecifications;

namespace GeoEnjoy.EntityFrameworkCore;

public class EfCorePointOfInterestRepository : EfCoreRepository<PointOfInterest>,
    IPointOfInterestRepository
{
    private readonly GeoEnjoyDbContext _context;

    public EfCorePointOfInterestRepository(GeoEnjoyDbContext context,
        IEntityExpands<PointOfInterest> expands,
        IEntitySortings<PointOfInterest> sortings)
        : base(context, expands, sortings)
    {
        _context = context;
    }

    public ValueTask<List<PointOfInterest>> GetInRadiusAsync(RadiusDto radius,
        ASpec<PointOfInterest>? spec = null,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<List<PointOfInterestAssessment>> GetPointOfInterestsAssessmentsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
