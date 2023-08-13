namespace HealthCareApplication.Domains.Models;

public class Address
{
    public int AddressId { get; private set; }
    public string Street { get; private set; }
    public string Ward { get; private set; }
    public string District { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Address() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Address(int addressId, string street, string ward, string district, string city, string country)
    {
        AddressId = addressId;
        Street = street;
        Ward = ward;
        District = district;
        City = city;
        Country = country;
    }

    public void Update(Address address)
    {
        Street = address.Street;
        Ward = address.Ward;
        District = address.District;
        City = address.City;
        Country = address.Country;
    }
}
