using AutoMapper;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Addresses;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication.Mapping;

public class ModelToViewModelProfile : Profile
{
    public ModelToViewModelProfile() 
    {
        CreateMap<Person, PersonViewModel>();

        CreateMap<Address, AddressViewModel>();

        CreateMap<BloodPressure, BloodPressureViewModel>();
        CreateMap<BloodPressure, BloodPressureMetricViewModel>();
        CreateMap<BloodSugar, BloodSugarViewModel>();
        CreateMap<BodyTemperature, BodyTemperatureViewModel>();
    }
}
