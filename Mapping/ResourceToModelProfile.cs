using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
using HealthCareApplication.Resource.Persons.Patients;
using HealthCareApplication.Resource.Users;

namespace HealthCareApplication;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile() 
    {
        CreateMap<DoctorRegistrationViewModel, Person>();

        CreateMap<AddNewRelativeViewModel, Person>();
        CreateMap<AddNewPatientViewModel, Person>();
        CreateMap<UpdateProfileViewModel, Person>();
    }
}
