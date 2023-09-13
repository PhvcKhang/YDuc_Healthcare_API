using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.ManyMany;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCareApplication.Domains.Persistence.Contexts.Configurations;

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.PersonId);

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Age).IsRequired();
        builder.Property(x => x.PersonType).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(255).IsRequired();

        builder.HasMany(x => x.BloodPressures).WithOne(x => x.Person);
        builder.HasMany(x => x.BloodSugars).WithOne(x => x.Person);
        builder.HasMany(x => x.BodyTemperatures).WithOne(x => x.Person);
        builder.HasMany(x => x.Notifications).WithOne(x => x.Doctor);
        builder.HasMany(x => x.Patients).WithMany();
        //builder.HasMany(x => x.Relatives).WithMany(x => x.Patients)
        //    .UsingEntity<PatientRelative>(
        //    l => l.HasOne(x => x.Patient).WithMany().HasForeignKey(x => x.PatientId),
        //    r => r.HasOne(x => x.Relative).WithMany().HasForeignKey(x => x.RelativeId)
        //    );

    }
}
