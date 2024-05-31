using GeoEnjoy.Domain.Entities;
using GeoEnjoy.EntityFrameworkCore.Npgsql.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoEnjoy.EntityFrameworkCore;

public class GeoEnjoyDbContext : DbContext
{
    public DbSet<PostGISPointOfInterest> PointOfInterests { get; set; }
    public DbSet<FavoritePointOfInterest> FavoritePointOfInterests { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<MediaFile> MediaFiles { get; set; }
    public DbSet<SocialActivity> SocialActivities { get; set; }

    public GeoEnjoyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeoEnjoyDbContext).Assembly);
    }
}
