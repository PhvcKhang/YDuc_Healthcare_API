﻿// <auto-generated />
using System;
using HealthCareApplication.Domains.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthCareApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230917134748_deleteDiastolic")]
    partial class deleteDiastolic
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HealthCareApplication.Domains.Models.BloodPressure", b =>
                {
                    b.Property<string>("BloodPressureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ImageLink")
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

            modelBuilder.Entity("HealthCareApplication.Domains.Models.Notification", b =>
                {
                    b.Property<string>("NotificaitonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("DoctorPersonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Seen")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SendAt")
                        .HasColumnType("datetime2");

                    b.HasKey("NotificaitonId");

                    b.HasIndex("DoctorPersonId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("HealthCareApplication.Domains.Models.Person", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18,2)");

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

            modelBuilder.Entity("HealthCareApplication.Domains.Models.Notification", b =>
                {
                    b.HasOne("HealthCareApplication.Domains.Models.Person", "Doctor")
                        .WithMany("Notifications")
                        .HasForeignKey("DoctorPersonId");

                    b.Navigation("Doctor");
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

                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
