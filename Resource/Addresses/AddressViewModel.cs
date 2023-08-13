namespace HealthCareApplication.Resource.Addresses;

public class AddressViewModel
{
    public string Street { get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public AddressViewModel(string street, string ward, string district, string city, string country)
    {
        Street = street;
        Ward = ward;
        District = district;
        City = city;
        Country = country;
    }
}

