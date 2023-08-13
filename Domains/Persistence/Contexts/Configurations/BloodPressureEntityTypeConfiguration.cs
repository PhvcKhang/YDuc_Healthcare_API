using HealthCareApplication.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCareApplication.Domains.Persistence.Contexts.Configurations;

public class BloodPressureEntityTypeConfiguration : IEntityTypeConfiguration<BloodPressure>
{
    public void Configure(EntityTypeBuilder<BloodPressure> builder)
    {
        builder.HasKey(x => x.BloodPressureId);
        builder.Property(x => x.BloodPressureId)
            .HasDefaultValueSql("NEWID()");
    }
}
