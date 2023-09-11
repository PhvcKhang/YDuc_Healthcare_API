using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile() 
    {
        CreateMap<CreatePersonViewModel, Person>();
    }
}
