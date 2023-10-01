using AutoMapper;
using HealthCareApplication.Domains.Models;

using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;
using HealthCareApplication.Resource.Notification;
using HealthCareApplication.Resource.Persons;
using HealthCareApplication.Resource.Persons.Doctors;
using HealthCareApplication.Resource.Persons.Relatives;
using HealthCareApplication.Resource.SpO2;

namespace HealthCareApplication.Mapping;

public class ModelToViewModelProfile : Profile
{
    public ModelToViewModelProfile() 
    {
        CreateMap<Person, PatientProfileViewModel>();
        CreateMap<Person, PatientsViewModel>();
        CreateMap<Person, DoctorIProfileViewModel>();
        CreateMap<Person, DoctorsViewModel>();
        CreateMap<Person, RelativesViewModel>();
        CreateMap<Person, RelativeProfileViewModel>();


        CreateMap<Notification, NotificationViewModel>(); 

        CreateMap<BloodPressure, BloodPressureViewModel>();
        CreateMap<BloodPressure, BloodPressureMetricViewModel>();
        CreateMap<BloodSugar, BloodSugarViewModel>();
        CreateMap<BodyTemperature, BodyTemperatureViewModel>();
        CreateMap<SpO2, SpO2ViewModel>();
    }
}
