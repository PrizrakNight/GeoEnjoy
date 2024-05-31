using GeoEnjoy.Domain.Entities;
using GeoEnjoy.Domain.Entities.PointOfInterests;
using GeoEnjoy.EntityFrameworkCore.Npgsql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoEnjoy.EntityFrameworkCore.Configurations;

internal class PostGISPointOfInterestConfiguration : IEntityTypeConfiguration<PostGISPointOfInterest>
{
    public void Configure(EntityTypeBuilder<PostGISPointOfInterest> builder)
    {
        builder.UseTptMappingStrategy();

        builder.HasKey(x => x.Id);

        builder.HasBaseType<PointOfInterest>();

        builder.Ignore(x => x.Longitude);
        builder.Ignore(x => x.Latitude);

        builder.HasMany(x => x.Reviews)
            .WithOne(nameof(Review.PointOfInterest));

        builder.HasMany(x => x.SocialActivities)
            .WithOne(nameof(Review.PointOfInterest));

        builder.HasMany(x => x.FavoritePointOfInterests)
            .WithOne(nameof(Review.PointOfInterest));
    }
}
