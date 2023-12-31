﻿using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication.Domains.Repositories;

public interface IPersonRepository
{
    #region Admin
    #endregion Admin

    #region User
    public Task<Person?> GetAsync(string personId);
    public Task<Person?> GetByPhoneNumber(string phoneNumber);
    public Task<Person?> GetPersonWithPatientsAsync(string personId);
    public Task<Person> RemoveRelationshipAsync(string carerId, string patientId);
    #endregion Person

    #region Patient
    public Task<List<Person>> GetAllAsync();
    public Task<List<Person>> GetPatientInfoAsync(string personId);
    public Task<int> GetTheNumberOfRelatives(Person patient);
    public Task<List<Person>> GetRelativesByPatientIdAsync(string patientId);
    public Person CreateRelationshipAsync(Person person, Person patient);
    #endregion Patient

    #region Doctor
    public Task<Person?> FindByIdAsync(string patientId);
    public Task<Person> AddPatientAsync(string personId, string patientId);
    public Task<Person?> GetDoctorInfoAsync(string doctorId);
    public Task<List<Person>?> GetAllDoctorsAsync();
    #endregion Doctor

    #region Relative
    public Task<List<Person>?> GetAllRelativesAsync();
    public Task<Person?> GetRelativeProfileAsync(string relativeId);

    #endregion Relative

    #region Functions
    public Task<bool> IsExisting(string phoneNumber);
    public Task<bool> DeleteNotificationsRelatingToThisPatient(string patientId);
    public Task<bool> DeleteNotificationsOfRelative(string relativeId,string patientId);
    #endregion Functions



}
