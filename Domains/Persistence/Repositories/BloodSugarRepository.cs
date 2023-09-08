using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApplication.Domains.Persistence.Repositories;

public class BloodSugarRepository : BaseRepository, IBloodSugarRepository
{
    public BloodSugarRepository(ApplicationDbContext context) : base(context)
    { 
    }

    public async Task<BloodSugar> Add(BloodSugar bloodSugar)
    {
        if (await ExistsAsync(bloodSugar.BloodSugarId))
        {
            throw new EntityDuplicationException(nameof(BloodSugar), bloodSugar.BloodSugarId);
        }

        return _context.BloodSugars
            .Add(bloodSugar)
            .Entity;
    }

    public async Task<BloodSugar?> GetAsync(string bloodSugarId)
    {
        return await _context.BloodSugars
            .Include(x => x.Person)
            .FirstOrDefaultAsync(x => x.BloodSugarId == bloodSugarId);
    }

    public async Task<BloodSugar?> GetNewestAsync()
    {
        return await _context.BloodSugars
            .Include(x => x.Person)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<List<BloodSugar>> GetListByTimeQueryAsync(string personId, TimeQuery timeQuery)
    {
        return await _context.BloodSugars
            .Include(x => x.Person)
            .Where(x => x.Person.PersonId == personId
                && x.Timestamp >= timeQuery.StartTime
                && x.Timestamp <= timeQuery.EndTime)
            .OrderByDescending(x => x.Timestamp)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(string bloodSugarId)
    {
        return await _context.BloodSugars
            .AnyAsync(x => x.BloodSugarId == bloodSugarId);
    }
}
