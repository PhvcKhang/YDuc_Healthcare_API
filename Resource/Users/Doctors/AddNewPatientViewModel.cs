using HealthCareApplication.Domains.Models;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HealthCareApplication.Resource.Persons.Doctors
{
    [DataContract]
    public class AddNewPatientViewModel
    {
        [DataMember]
        [JsonIgnore]
        public string Username { get; set; }
        [DataMember]
        [JsonIgnore]
        public string Password { get; set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string PhoneNumber { get; private set; }
        [DataMember]
        [JsonIgnore]
        public EPersonType PersonType { get; private set; }

        public AddNewPatientViewModel( string name, string phoneNumber)
        {
            Username = phoneNumber;
            Password = phoneNumber;
            Name = name;
            PersonType = EPersonType.Patient;
            PhoneNumber = phoneNumber;
        }
    }
}
