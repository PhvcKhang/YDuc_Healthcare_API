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
        var patient = await _context.Users.FirstOrDefaultAsync(x => x.Id == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);
        var doctor = await _context.Users.FirstOrDefaultAsync(x => x.Patients.Contains(patient) && x.PersonType == EPersonType.Doctor) ?? throw new ResourceNotFoundException("Logical Error - There is no doctor being responsible for this patient");
        var relatives = await _context.Users.Where(x => x.Patients.Contains(patient) && x.PersonType == EPersonType.Relative).ToListAsync();

        var lastBloodPressure = await _context.BloodPressures.OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync();
        var lastBloodSugar = await _context.BloodSugars.OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync();
        var lastBodyTemperature = await _context.BodyTemperatures.OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync();
        var lastSpO2 = await _context.SpO2s.OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync();

        if(relatives.Count() == 2 && doctor is not null)
        {
            return new List<Person>() { patient, doctor, relatives[0], relatives[1] };
        }
        else if(relatives.Count() == 1 && doctor is not null)
        {
            return new List<Person>() { patient, doctor, relatives[0]};
        }
        else if(doctor is null)
        {
            throw new Exception("This patient doesn't belong to any doctor");
        }
        else
        {
            //Testing purpose only - This case is unvalid
            return new List<Person>() { patient, doctor };
        }

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
    public async Task<Person?> GetRelativeAsync(string patientId)
    {
        return await _context.Users
            .Include(x => x.Patients)
            .Where(x => x.Id == patientId)
            .FirstOrDefaultAsync();
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

    public async Task<bool> IsExisting(string phoneNumber)
    {
        return await _context.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
    }





    #endregion Relative


}
