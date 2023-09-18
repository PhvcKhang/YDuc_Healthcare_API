using HealthCareApplication.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCareApplication.Domains.Persistence.Contexts.Configurations
{
    public class SpO2EntityTypeConfiguration : IEntityTypeConfiguration<SpO2>
    {
        public void Configure(EntityTypeBuilder<SpO2> builder)
        {
            builder.HasKey(x => x.SpO2Id);
            builder.Property(x => x.SpO2Id)
                .HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.Notification).WithOne(x => x.SpO2).HasForeignKey<Notification>(x => x.SpO2Id);
        }
    }
}
