using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Addresses;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile() 
    {
        CreateMap<CreatePersonViewModel, Person>();

        CreateMap<CreateAddressViewModel, Address>();
        CreateMap<UpdateAddressViewModel, Address>();
    }
}
