using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Notification;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HealthCareApplication.Resource.Persons.Patients
{
    [DataContract]
    public class AddNewRelativeViewModel
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
        [JsonIgnore]
        public int Age { get; private set; }

        [DataMember]
        [JsonIgnore]
        public EPersonType PersonType { get; private set; }
        [DataMember]
        public string PhoneNumber { get; private set; }

        public AddNewRelativeViewModel( string name, string phoneNumber)
        {
            Username = phoneNumber;
            Password = phoneNumber;
            Name = name;
            PersonType = EPersonType.Relative;
            PhoneNumber = phoneNumber;
        }
    }
}
