using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApplication.Domains.Persistence.Repositories;

public class PersonRepository : BaseRepository, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }

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
}
