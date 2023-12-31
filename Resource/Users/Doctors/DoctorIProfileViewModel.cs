﻿using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.BloodPressure;
using HealthCareApplication.Resource.BloodSugar;
using HealthCareApplication.Resource.BodyTemperature;
using HealthCareApplication.Resource.Notification;

namespace HealthCareApplication.Resource.Persons.Doctors
{
    public class DoctorIProfileViewModel
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Address { get; private set; } = string.Empty;
        public EPersonGender Gender { get; private set; }
        public EPersonType PersonType { get; private set; }
        public string? PhoneNumber { get; private set; }
        public List<PatientsViewModel>? Patients { get; private set; }
    }
}
