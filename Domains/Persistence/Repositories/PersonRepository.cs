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

    #region Person
    public async Task<Person> Add(Person person)
    {
         if (await ExistsAsync(person.PersonId))
         {
             throw new EntityDuplicationException(nameof(Person), person.PersonId);
         }

         return _context.Persons
             .Add(person)
             .Entity;
    }
    public async Task<Person?> GetAsync(string personId)
    {
        return await _context.Persons
            .FirstOrDefaultAsync(x => x.PersonId == personId);
    }

    public async Task<List<Person>> GetListByIdAsync(List<string> personIds)
    {
        var persons = await _context.Persons
            .Include(x => x.BloodPressures)
            .Include(x => x.BloodSugars)
            .Include(x => x.BodyTemperatures)
            .Include(x => x.Patients)
            .Where(x => personIds.Contains(x.PersonId))
            .ToListAsync();

        var notFoundIds = personIds
            .Where(id => !persons.Any(pc => pc.PersonId == id));

        if (notFoundIds.Any())
        {
            throw new EntitiesNotFoundException(nameof(Person), notFoundIds.ToList());
        }

        return persons;
    }

    public Person Update(Person person)
    {
        return _context.Persons
                .Update(person)
                .Entity;
    }

    public async Task<bool> ExistsAsync(string personId)
    {
        return await _context.Persons
            .AnyAsync(x => x.PersonId == personId);
    }

    public async Task<Person> RemoveRelationshipAsync (string personId, string patientId)
    {
        Person person = await _context.Persons.Include(x => x.Patients).FirstOrDefaultAsync(x => x.PersonId == personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);
        Person patient = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);
        person.Patients.Remove(patient);
        return _context.Persons
            .Update(person)
            .Entity;
    }
    public async Task DeleteAsync(string personId)
    {
        var person = await _context.Persons
            .Include(x => x.BloodPressures)
            .Include(x => x.BloodSugars)
            .Include(x => x.BodyTemperatures)
            .FirstOrDefaultAsync(x => x.PersonId == personId);

        if (person is not null)
        {
            _context.Persons.Remove(person);
        }
    }
    #endregion Person
    #region Patient
    public async Task<List<Person>> GetAllAsync()
    {
        return await _context.Persons
            .Where(x => x.PersonType == EPersonType.Patient)
            .ToListAsync();

    }
    public async Task<Person?> GetPatientInfoAsync(string patientId)
    {
        var patient = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);
        //var relatives = await _context.Persons.Where(x => x.Patients.Contains(patient)).ToListAsync();

        var lastBloodPressure = await _context.BloodPressures.OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync();
        var lastBloodSugar = await _context.BloodSugars.OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync();
        var lastBodyTemperature = await _context.BodyTemperatures.OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync();

        patient.BloodPressures.RemoveAll(x => x != lastBloodPressure);
        patient.BloodSugars.RemoveAll(x => x != lastBloodSugar);
        patient.BodyTemperatures.RemoveAll(x => x != lastBodyTemperature);

        return patient;

    }
    public Person AddPatient(string relativeId, string patientId)
    {
        Person relative = _context.Persons.Include(x => x.Patients).FirstOrDefault(x => x.PersonId == relativeId) ?? throw new ResourceNotFoundException(nameof(Person), relativeId);

        Person patient = _context.Persons.FirstOrDefault(x => x.PersonId == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);
        relative.Patients.Add(patient);

        return _context.Persons.Update(relative).Entity;
    }

    #endregion Patient

    #region Doctor
    public async Task<Person?> GetDoctorInfoAsync(string doctorId)
    {
        return await _context.Persons
            .Include(x => x.Patients)
            .FirstOrDefaultAsync(x => x.PersonId == doctorId);
    }
    public async Task<List<Person>?> GetAllDoctorsAsync()
    {
        return await _context.Persons
            .Where(x => x.PersonType == EPersonType.Doctor)
            .ToListAsync();
    }
    public async Task<Person> AddPatientAsync(string personId, string patientId)
    {

        Person person = await _context.Persons.Include(x => x.Patients).FirstOrDefaultAsync(x => x.PersonId == personId) ?? throw new ResourceNotFoundException(nameof(Person), personId);

        Person patient = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);


        person.Patients.Add(patient);

        return _context.Persons.Update(person).Entity;

    }

    public async Task<Person?> FindByIdAsync(string patientId)
    {
        var patient = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == patientId) ?? throw new ResourceNotFoundException(nameof(Person), patientId);

        return await _context.Persons
                .FirstOrDefaultAsync(x => x.Patients.Contains(patient));
    }
    #endregion Doctor

    #region Relative
    public async Task<List<Person>?> GetAllRelativesAsync()
    {
        return await _context.Persons
            .Include(x => x.Patients)
            .Where(x => x.PersonType == EPersonType.Relative)
            .ToListAsync();
    }
    public async Task<Person?> GetRelativeAsync(string patientId)
    {
        return await _context.Persons
            .Include(x => x.Patients)
            .Where(x => x.PersonId == patientId)
            .FirstOrDefaultAsync();
    }


    #endregion Relative


}
