﻿using HealthCareApplication.Domains.Models;
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

        builder.HasMany(x => x.Notifications).WithOne(x => x.BodyTemperature).HasForeignKey(x => x.BodyTemperatureId).OnDelete(DeleteBehavior.Cascade);
    }
}
