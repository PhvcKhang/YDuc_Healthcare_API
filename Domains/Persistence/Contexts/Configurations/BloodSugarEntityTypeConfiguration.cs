using HealthCareApplication.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCareApplication.Domains.Persistence.Contexts.Configurations;

public class BloodSugarEntityTypeConfiguration : IEntityTypeConfiguration<BloodSugar>
{
    public void Configure(EntityTypeBuilder<BloodSugar> builder)
    {
        builder.HasKey(x => x.BloodSugarId);
        builder.Property(x => x.BloodSugarId)
            .HasDefaultValueSql("NEWID()");

        builder.HasOne(x => x.Notification).WithOne(x => x.BloodSugar).HasForeignKey<Notification>(x => x.BloodSugarId);
    }
}
