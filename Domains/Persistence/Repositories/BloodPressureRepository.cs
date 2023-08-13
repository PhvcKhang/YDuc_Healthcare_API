using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApplication.Domains.Persistence.Repositories;

public class BloodPressureRepository : BaseRepository, IBloodPressureRepository
{
    public BloodPressureRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<BloodPressure> Add(BloodPressure bloodPressure)
    {
        if (await ExistsAsync(bloodPressure.BloodPressureId))
        {
            throw new EntityDuplicationException(nameof(BloodPressure), bloodPressure.BloodPressureId);
        }

        return _context.BloodPressures
            .Add(bloodPressure)
            .Entity;
    }

    public async Task<BloodPressure?> GetAsync(string bloodPressureId)
    {
        return await _context.BloodPressures
            .Include(x => x.Person)
            .FirstOrDefaultAsync(x => x.BloodPressureId == bloodPressureId);
    }

    public async Task<BloodPressure?> GetNewestAsync()
    {
        return await _context.BloodPressures
            .Include(x => x.Person)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<List<BloodPressure>> GetListByTimeQueryAsync(string personId, TimeQuery timeQuery)
    {
        return await _context.BloodPressures
            .Include(x => x.Person)
            .Where(x => x.Person.PersonId == personId
                && x.Timestamp >= timeQuery.StartTime
                && x.Timestamp <= timeQuery.EndTime)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(string bloodPressureId)
    {
        return await _context.BloodPressures
            .AnyAsync(x => x.BloodPressureId == bloodPressureId);
    }
}
