using GeoEnjoy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoEnjoy.EntityFrameworkCore.Configurations;

internal class SocialActivityConfiguration : IEntityTypeConfiguration<SocialActivity>
{
    public void Configure(EntityTypeBuilder<SocialActivity> builder)
    {
        builder.HasNoKey();

        builder.HasOne(x => x.Review)
            .WithMany(x => x.SocialActivities);

        builder.HasOne(x => x.PointOfInterest)
            .WithMany(x => x.SocialActivities);
    }
}
