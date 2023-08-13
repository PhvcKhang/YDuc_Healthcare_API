using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Addresses;

namespace HealthCareApplication.Resource.Persons;

public class PersonViewModel
{
    public string PersonId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public EPersonType PersonType { get; set; }
    public AddressViewModel Address { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }

    public PersonViewModel(string personId, string name, int age, EPersonType personType, AddressViewModel address, decimal weight, decimal height)
    {
        PersonId = personId;
        Name = name;
        Age = age;
        PersonType = personType;
        Address = address;
        Weight = weight;
        Height = height;
    }
}
