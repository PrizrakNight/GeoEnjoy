using GeoEnjoy.Application.Dto;
using GeoEnjoy.Application.Repositories;
using GeoEnjoy.Domain.Entities.PointOfInterests;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NSpecifications;

namespace GeoEnjoy.EntityFrameworkCore.Repositories;

public class EfCorePointOfInterestRepository(GeoEnjoyDbContext context,
    IEntityExpands<PointOfInterest> expands,
    IEntitySortings<PointOfInterest> sortings) : EfCoreRepository<PointOfInterest>(context, expands, sortings),
    IPointOfInterestRepository
{
    private readonly GeoEnjoyDbContext _context = context;

    private const double METERS_PER_KILOMETER = 1000;

    public async ValueTask<List<PointOfInterest>> GetInRadiusAsync(RadiusDto radius,
        ASpec<PointOfInterest>? spec = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.PointOfInterests
            .AsNoTracking()
            .AsQueryable();

        if (spec != null)
        {
            query = (IQueryable<Npgsql.Entities.PostGISPointOfInterest>)query.Where(spec);
        }

        var meters = radius.RadiusMeters / METERS_PER_KILOMETER;
        var center = new Point(radius.Point.Longitude, radius.Point.Latitude);
        var circle = center.Buffer(meters);

        query = query.Where(x => x.PointOnMap.IsWithinDistance(circle, meters));

        return await query
            .Cast<PointOfInterest>()
            .ToListAsync(cancellationToken);
    }

    public async ValueTask<List<PointOfInterestAssessment>> GetPointOfInterestsAssessmentsAsync(IEnumerable<Guid> pointIds,
        CancellationToken cancellationToken = default)
    {
        return await _context.PointOfInterestAssessments
            .AsNoTracking()
            .Where(x => pointIds.Contains(x.PointId))
            .ToListAsync(cancellationToken);
    }
}
