using HealthCareApplication.Domains.Models;
using System.Runtime.Serialization;

namespace HealthCareApplication.Resource.Users
{
    [DataContract]
    public class UpdateProfileViewModel
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public int Age { get; private set; }
        [DataMember]
        public string Address { get; private set; }
 
        [DataMember]
        public EPersonGender Gender { get; private set; }
        [DataMember]
        public decimal Weight { get; private set; }
        [DataMember]
        public decimal Height { get; private set; }

        public UpdateProfileViewModel(string name, int age, string address, EPersonGender gender, decimal weight, decimal height)
        {
            Name = name;
            Age = age;
            Address = address;
            Gender = gender;
            Weight = weight;
            Height = height;
        }
    }
}
