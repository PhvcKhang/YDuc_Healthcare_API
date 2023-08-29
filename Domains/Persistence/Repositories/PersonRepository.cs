using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
using HealthCareApplication.Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
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
            .Include(x => x.Address)
            .Include(x => x.BloodPressures)
            .Include(x => x.BloodSugars)
            .Include(x => x.BodyTemperatures)
            .Include(x => x.Patients)
            .FirstOrDefaultAsync(x => x.PersonId == personId);
    }

    public async Task<List<Person>> GetListByIdAsync(List<string> personIds)
    {
        var persons = await _context.Persons
            .Include(x => x.Address)
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
    public async Task DeleteAsync(string personId)
    {
        var person = await _context.Persons
            .Include(x => x.Address)
            .Include(x => x.BloodPressures)
            .Include(x => x.BloodSugars)
            .Include(x => x.BodyTemperatures)
            .FirstOrDefaultAsync(x => x.PersonId == personId);

        if (person is not null)
        {
            _context.Persons.Remove(person);
            _context.Addresses.Remove(person.Address);
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
        return await _context.Persons
        .Include(x => x.Address)
        .Include(x => x.BloodPressures.OrderByDescending(x => x.Timestamp))
        .Include(x => x.BloodSugars.OrderByDescending(x => x.Timestamp))
        .Include(x => x.BodyTemperatures.OrderByDescending(x => x.Timestamp))
        .FirstOrDefaultAsync(x => x.PersonId == patientId);
    }


    #endregion Patient

    #region Doctor
    public async Task<Person?> GetDoctorInfoAsync(string doctorId)
    {
        return await _context.Persons
            .Include(x => x.Patients)
            .Include(x => x.Address)
            .Where(x => x.PersonId == doctorId)
            .FirstOrDefaultAsync();
    }
    public async Task<List<Person>?> GetAllDoctorsAsync()
    {
        return await _context.Persons
            .Where(x => x.PersonType == EPersonType.Doctor)
            .ToListAsync();
    }
    public async Task<Person> AddPatient(string doctorId, string patientId)
    {

        //var notFound = await _context.Persons.AnyAsync(x => x.PersonId != doctorId || x.PersonId != patientId);
        //if (notFound is true)
        //{
        //    throw new ResourceNotFoundException(nameof(Person), doctorId);
        //}

        Person doctor = await _context.Persons.Include(x => x.Patients).Where(x => x.PersonId == doctorId).FirstOrDefaultAsync();
        Person patient = await _context.Persons.Where(x => x.PersonId == patientId).FirstOrDefaultAsync();


        doctor.Patients.Add(patient);

        return _context.Persons.Update(doctor).Entity;

    }

    public async Task<Person?> FindByIdAsync(string patientId)
    {
        var patient = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == patientId);

        //List<Person> doctors =_context.Persons.Where(x => x.PersonType == EPersonType.Doctor).ToList();

        //var doctor = from d in doctors
        //             where d.Patients.Contains(patient)
        //             select d;

        return await _context.Persons
                .FirstOrDefaultAsync(x => x.Patients.Contains(patient));
    }
    #endregion Doctor


}
