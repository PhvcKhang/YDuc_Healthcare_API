﻿using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication.Domains.Repositories;

public interface IPersonRepository
{
    #region Person
    public Task<Person?> GetAsync(string personId);
    public Task<Person?> GetByPhoneNumber(string phoneNumber);
    public Task<Person?> GetPersonWithPatientsAsync(string personId);
    #endregion Person

    #region Patient
    public Task<List<Person>> GetAllAsync();
    public Task<List<Person>> GetPatientInfoAsync(string personId);
    public Task<int> GetTheNumberOfRelatives(Person patient);
    public Task<Person?> GetRelativeAsync(string patientId);
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

    #endregion Relative
    #region Functions
    public Task<bool> IsExisting(string phoneNumber);
    #endregion Functions



}
