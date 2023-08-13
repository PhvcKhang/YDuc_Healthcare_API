using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.Addresses;

[DataContract]
public class CreateAddressViewModel
{
    [DataMember]
    public string Street { get; set; }
    [DataMember]
    public string Ward { get; set; }
    [DataMember]
    public string District { get; set; }
    [DataMember]
    public string City { get; set; }
    [DataMember]
    public string Country { get; set; }

    public CreateAddressViewModel(string street, string ward, string district, string city, string country)
    {
        Street = street;
        Ward = ward;
        District = district;
        City = city;
        Country = country;
    }
}
