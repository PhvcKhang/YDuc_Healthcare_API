﻿// <auto-generated />
using System;
using HealthCareApplication.Domains.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthCareApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HealthCareApplication.Domains.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ward")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("HealthCareApplication.Domains.Models.BloodPressure", b =>
                {
                    b.Property<string>("BloodPressureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("Diastolic")
                        .HasColumnType("int");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PulseRate")
                        .HasColumnType("int");

                    b.Property<int>("Systolic")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("BloodPressureId");

                    b.HasIndex("PersonId");

                    b.ToTable("BloodPressures");
                });

            modelBuilder.Entity("HealthCareApplication.Domains.Models.BloodSugar", b =>
                {
                    b.Property<string>("BloodSugarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BloodSugarId");

                    b.HasIndex("PersonId");

                    b.ToTable("BloodSugars");
                });

            modelBuilder.Entity("HealthCareApplication.Domains.Models.BodyTemperature", b =>
                {
                    b.Property<string>("BodyTemperatureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BodyTemperatureId");

                    b.HasIndex("PersonId");

                    b.ToTable("BodyTemperatures");
                });

            modelBuilder.Entity("HealthCareApplication.Domains.Models.Person", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PersonType")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PersonId");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PersonPerson", b =>
                {
                    b.Property<string>("PatientsPersonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PatientsPersonId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonPerson");
                });

            modelBuilder.Entity("HealthCareApplication.Domains.Models.BloodPressure", b =>
                {
                    b.HasOne("HealthCareApplication.Domains.Models.Person", "Person")
                        .WithMany("BloodPressures")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("HealthCareApplication.Domains.Models.BloodSugar", b =>
                {
                    b.HasOne("HealthCareApplication.Domains.Models.Person", "Person")
                        .WithMany("BloodSugars")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("HealthCareApplication.Domains.Models.BodyTemperature", b =>
                {
                    b.HasOne("HealthCareApplication.Domains.Models.Person", "Person")
                        .WithMany("BodyTemperatures")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("HealthCareApplication.Domains.Models.Person", b =>
                {
                    b.HasOne("HealthCareApplication.Domains.Models.Address", "Address")
                        .WithOne()
                        .HasForeignKey("HealthCareApplication.Domains.Models.Person", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("PersonPerson", b =>
                {
                    b.HasOne("HealthCareApplication.Domains.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PatientsPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthCareApplication.Domains.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HealthCareApplication.Domains.Models.Person", b =>
                {
                    b.Navigation("BloodPressures");

                    b.Navigation("BloodSugars");

                    b.Navigation("BodyTemperatures");
                });
#pragma warning restore 612, 618
        }
    }
}
