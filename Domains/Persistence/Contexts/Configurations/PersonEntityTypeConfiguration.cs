﻿using HealthCareApplication.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCareApplication.Domains.Persistence.Contexts.Configurations;

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Age).IsRequired();
        builder.Property(x => x.PersonType).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(255).IsRequired();

        builder.HasMany(x => x.BloodPressures).WithOne(x => x.Person).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.BloodSugars).WithOne(x => x.Person).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.BodyTemperatures).WithOne(x => x.Person).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.SpO2s).WithOne(x => x.Person).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Notifications).WithOne(x => x.Carer).HasForeignKey(x => x.CarerId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Patients).WithMany();
    }
}
