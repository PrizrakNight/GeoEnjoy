using GeoEnjoy.Domain.Entities;
using GeoEnjoy.Domain.Entities.PointOfInterests;
using GeoEnjoy.EntityFrameworkCore.Npgsql.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoEnjoy.EntityFrameworkCore;

public class GeoEnjoyDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<PostGISPointOfInterest> PointOfInterests { get; set; }
    public DbSet<FavoritePointOfInterest> FavoritePointOfInterests { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<MediaFile> MediaFiles { get; set; }
    public DbSet<SocialActivity> SocialActivities { get; set; }
    public DbSet<PointOfInterestAssessment> PointOfInterestAssessments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeoEnjoyDbContext).Assembly);
    }
}
