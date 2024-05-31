using GeoEnjoy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoEnjoy.EntityFrameworkCore.Configurations;

internal class FavoritePointOfInterestConfiguration : IEntityTypeConfiguration<FavoritePointOfInterest>
{
    public void Configure(EntityTypeBuilder<FavoritePointOfInterest> builder)
    {
        builder.HasKey(x => new { x.UserId, x.PointId });

        builder.HasOne(x => x.PointOfInterest)
            .WithMany(x => x.FavoritePointOfInterests)
            .HasForeignKey(x => x.PointId);
    }
}
