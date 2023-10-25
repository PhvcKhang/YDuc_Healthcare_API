using HealthCareApplication.Domains.Models;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HealthCareApplication.Resource.Users.Admin
{
    public class AdminRegistrationViewModel
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        [JsonIgnore]
        public EPersonType PersonType { get; set; }

        [DataMember]
        public string PhoneNumber { get; private set; }

        public AdminRegistrationViewModel(string username, string password, string name, string phoneNumber)
        {
            Username = username;
            Password = password;
            Name = name;
            PersonType = EPersonType.Admin;
            PhoneNumber = phoneNumber;
        }
    }
}
