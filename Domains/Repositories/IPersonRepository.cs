﻿using HealthCareApplication.Domains.Models;
using HealthCareApplication.Resource.Persons;

namespace HealthCareApplication.Domains.Repositories;

public interface IPersonRepository
{
    #region Person
    public Task<Person> Add(Person person);
    public Task<Person?> GetAsync(string personId);
    public Task<List<Person>> GetListByIdAsync(List<string> personIds);
    public Person Update(Person person);
    public Task DeleteAsync(string personId);
    public Task<bool> ExistsAsync(string personId);
    #endregion Person

    #region Patient
    public Task<List<Person>> GetAllAsync();
    public Task<Person?> GetPatientInfoAsync(string personId);
    public Task<Person> AddPatient(string personId, string patientId);
    #endregion Patient

    #region Doctor
    public Task<Person?> FindByIdAsync(string patientId);
    public Task<Person?> GetDoctorInfoAsync(string doctorId);
    public Task<List<Person>?> GetAllDoctorsAsync();
    #endregion Doctor

    #region Relative
    public Task<List<Person>?> GetAllRelativesAsync();
    public Task<Person?> GetRelativeAsync(string patientId);
    #endregion Relative




}
