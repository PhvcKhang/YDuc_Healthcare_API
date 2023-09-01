using HealthCareApplication.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCareApplication.Domains.Persistence.Contexts.Configurations
{
    public class NotificationEntityTypeConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.NotificaitonId);

            builder.Property(x => x.Heading).HasMaxLength(255);
            builder.Property(x => x.Content).HasMaxLength(255);
            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.PatientName).IsRequired();
        }
    }
}
