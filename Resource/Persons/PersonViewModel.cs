using HealthCareApplication.Domains.Models;
using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.Persons;

public class PersonViewModel
{
    public string PersonId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public EPersonType PersonType { get; set; }
    public string PhoneNumber { get; private set; }
    public EPersonGender Gender { get; private set; }

}
