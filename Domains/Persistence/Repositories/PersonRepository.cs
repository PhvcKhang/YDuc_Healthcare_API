using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace HealthCareApplication.Domains.Persistence.Repositories;

public class PersonRepository : BaseRepository, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }

    #region Admin
    #endregion Admin

    #region User

    public async Task<Person?> GetAsync(string personId)
    {
        return await _context.Persons
            .FirstOrDefaultAsync(x => x.Id == personId);
    }
    public async Task<Person?> GetByPhoneNumber(string phoneNumber)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
  
    }
    public async Task<Person?> GetPersonWithPatientsAsync(string personId)
    {
        return await _context.Users.Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == personId);
    }

    public async Task<bool> ExistsAsync(string personId)
    {
        return await _context.Users
            .AnyAsync(x => x.Id == personId);
    }

    public async Task<Person> RemoveRelationshipAsync (string personId, string patientId)
    {
        Person person = await _context.Users.Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        Person patient = await _context.Users.FirstOrDefaultAsync(x => x.Id == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);
        person.Patients.Remove(patient);
        return _context.Users
            .Update(person)
            .Entity;
    }
    #endregion User

    #region Patient
    public async Task<List<Person>> GetAllAsync()
    {
        return await _context.Users
            .Where(x => x.PersonType == EPersonType.Patient)
            .ToListAsync();

    }
    public async Task<List<Person>> GetPatientInfoAsync(string patientId)
    {
        var patient = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);
        var doctor = await _context.Users
            .FirstOrDefaultAsync(x => x.Patients.Contains(patient) && x.PersonType == EPersonType.Doctor) ?? throw new ResourceNotFoundException("Logical Error - There is no doctor being responsible for this patient");
        var relatives = await _context.Users
            .Where(x => x.Patients.Contains(patient) && x.PersonType == EPersonType.Relative)
            .ToListAsync();

        var newestBloodPressure = await _context.BloodPressures
            .Where(x => x.Person == patient)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync();
        var newestBloodSugar = await _context.BloodSugars
                        .Where(x => x.Person == patient)
                        .OrderByDescending(x => x.Timestamp)
                        .FirstOrDefaultAsync();
        var newestBodyTemperature = await _context.BodyTemperatures
            .Where(x => x.Person == patient)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync();
        var newestSpO2 = await _context.SpO2s
            .Where(x => x.Person == patient)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync();

        var listOfPeople = new List<Person>() { };
        listOfPeople.Add(patient);
        listOfPeople.Add(doctor);
        listOfPeople.AddRange(relatives);

        return listOfPeople;
    }
    public async Task<int> GetTheNumberOfRelatives(Person patient)
    {
        var relatives = await _context.Users.Where(x => x.Patients.Contains(patient) && x.PersonType == EPersonType.Relative).ToListAsync();
        return relatives.Count();
    }
    public Person CreateRelationshipAsync(Person person, Person patient)
    {
        person.Patients.Add(patient);
        return _context.Users.Update(person).Entity;
    }


    public async Task<List<Person>> GetRelativesByPatientIdAsync(string patientId)
    {
        var patient = await _context.Users.FirstOrDefaultAsync(x => x.Id == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);
        var relatvies = await _context.Users.Where(x => x.Patients.Contains(patient) && x.PersonType == EPersonType.Relative).ToListAsync();
        return relatvies;
    }
    #endregion Patient

    #region Doctor
    public async Task<Person?> GetDoctorInfoAsync(string doctorId)
    {
        return await _context.Users
            .Include(x => x.Patients)
            .FirstOrDefaultAsync(x => x.Id == doctorId);
    }
    public async Task<List<Person>?> GetAllDoctorsAsync()
    {
        return await _context.Users
            .Where(x => x.PersonType == EPersonType.Doctor)
            .ToListAsync();
    }
    public async Task<Person> AddPatientAsync(string personId, string patientId)
    {

        Person person = await _context.Users.Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);

        Person patient = await _context.Users.FirstOrDefaultAsync(x => x.Id == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);


        person.Patients.Add(patient);

        return _context.Users.Update(person).Entity;

    }

    public async Task<Person?> FindByIdAsync(string patientId)
    {
        var patient = await _context.Users.FirstOrDefaultAsync(x => x.Id == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);

        return await _context.Users
                .FirstOrDefaultAsync(x => x.Patients.Contains(patient));
    }
    #endregion Doctor

    #region Relative
    public async Task<List<Person>?> GetAllRelativesAsync()
    {
        return await _context.Users
            .Include(x => x.Patients)
            .Where(x => x.PersonType == EPersonType.Relative)
            .ToListAsync();
    }

    public async Task<List<Person>?> GetPatientsByIdAsync(string personId)
    {
        var person = await _context.Users.Include(x => x.Patients).FirstOrDefaultAsync(x => x.Id == personId) ?? throw new ResourceNotFoundException(nameof(Person), personId); 
        List<Person> patients = new();
        foreach(Person patient in person.Patients)
        {
            patients.Add(patient);
        }
        return patients;
    }

    public async Task<Person?> GetRelativeProfileAsync(string relativeId)
    {
        var relative = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == relativeId);

        if (relative is null)
        {
            return relative;
        }

        await _context.Entry(relative).Collection(x => x.Patients).LoadAsync();
        return relative;
    }

    public async Task<bool> IsExisting(string phoneNumber)
    {
        return await _context.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
    }

    public async Task<bool> DeleteNotificationsRelatingToThisPatient(string patientId)
    {
        var notificationsToDelete = await _context.Notifications.Where(x => x.PatientId == patientId).ToListAsync();
        _context.RemoveRange(notificationsToDelete);
        return true;
    }

    public async Task<bool> DeleteNotificationsOfRelative(string relativeId, string patientId)
    {
        var notificationsToDelete = await _context.Notifications.Where(x => x.PatientId == patientId && x.CarerId == relativeId).ToListAsync();
        _context.RemoveRange(notificationsToDelete);
        return true;
    }

    #endregion Relative


}
