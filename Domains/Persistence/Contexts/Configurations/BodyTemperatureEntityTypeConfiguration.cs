using HealthCareApplication.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCareApplication.Domains.Persistence.Contexts.Configurations;

public class BodyTemperatureEntityTypeConfiguration : IEntityTypeConfiguration<BodyTemperature>
{
    public void Configure(EntityTypeBuilder<BodyTemperature> builder)
    {
        builder.HasKey(x => x.BodyTemperatureId);
        builder.Property(x => x.BodyTemperatureId)
            .HasDefaultValueSql("NEWID()");

        builder.HasOne(x => x.Notification).WithOne(x => x.BodyTemperature).HasForeignKey<Notification>(x => x.BodyTemperatureId);
    }
}
