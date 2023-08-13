using HealthCareApplication.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthCareApplication.Domains.Persistence.Contexts.Configurations;

public class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(x => x.AddressId);
        builder.Property(x => x.AddressId).ValueGeneratedOnAdd();
    }
}
