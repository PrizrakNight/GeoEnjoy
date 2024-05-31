using GeoEnjoy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoEnjoy.EntityFrameworkCore.Configurations;

internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.PointOfInterest)
            .WithMany(x => x.Reviews);

        builder.HasMany(x => x.SocialActivities)
            .WithOne(x => x.Review);
    }
}
