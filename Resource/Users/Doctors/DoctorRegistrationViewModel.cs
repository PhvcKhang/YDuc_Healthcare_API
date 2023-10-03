using HealthCareApplication.Domains.Models;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HealthCareApplication.Resource.Persons.Doctors
{
    [DataContract]
    public class DoctorRegistrationViewModel
    {
        [DataMember]
        [JsonIgnore]
        public string Username { get; set; }
        [DataMember]
        [JsonIgnore]
        public string Password { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [JsonIgnore]
        public EPersonType PersonType { get; set; } 

        [DataMember]
        public string PhoneNumber { get; private set; }

        public DoctorRegistrationViewModel(string name, string phoneNumber)
        {
            Username = phoneNumber;
            Password = phoneNumber;
            Name = name;
            PersonType = EPersonType.Doctor;
            PhoneNumber = phoneNumber;
        }
    }
}
