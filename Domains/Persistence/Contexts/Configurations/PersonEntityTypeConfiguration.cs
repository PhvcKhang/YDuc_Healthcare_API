using HealthCareApplication.Domains.Models;
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
        builder.Property(x => x.Avatar).IsRequired();

        builder.HasOne(x => x.Address).WithOne().HasForeignKey<Person>(x => x.AddressId);
        builder.HasMany(x => x.BloodPressures).WithOne(x => x.Person);
        builder.HasMany(x => x.BloodSugars).WithOne(x => x.Person);
        builder.HasMany(x => x.BodyTemperatures).WithOne(x => x.Person);
        builder.HasMany(x => x.Patients).WithMany();
    }
}
