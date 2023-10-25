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

            builder.HasMany(x => x.Notifications).WithOne(x => x.SpO2).HasForeignKey(x => x.SpO2Id);
        }
    }
}
