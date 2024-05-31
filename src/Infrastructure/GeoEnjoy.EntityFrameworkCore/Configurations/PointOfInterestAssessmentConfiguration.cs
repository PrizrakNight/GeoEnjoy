using GeoEnjoy.Domain.Entities.PointOfInterests;
using GeoEnjoy.EntityFrameworkCore.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoEnjoy.EntityFrameworkCore.Configurations;

internal class PointOfInterestAssessmentConfiguration : IEntityTypeConfiguration<PointOfInterestAssessment>
{
    public void Configure(EntityTypeBuilder<PointOfInterestAssessment> builder)
    {
        builder.ToView(Views.PointOfInterestAssessment);

        builder.HasKey(x => x.PointId);
    }
}
