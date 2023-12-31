﻿using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApplication.Domains.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<Person>
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<BloodPressure> BloodPressures { get; set; }
    public DbSet<BloodSugar> BloodSugars { get; set; }
    public DbSet<BodyTemperature> BodyTemperatures { get; set; }
    public DbSet<SpO2> SpO2s { get; set; }

    public DbSet<Notification> Notifications { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BloodPressureEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BloodSugarEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BodyTemperatureEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SpO2EntityTypeConfiguration());
    }
}
