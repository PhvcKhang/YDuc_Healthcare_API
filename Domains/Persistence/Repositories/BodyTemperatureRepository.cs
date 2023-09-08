using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApplication.Domains.Persistence.Repositories;

public class BodyTemperatureRepository : BaseRepository, IBodyTemperatureRepository
{
    public BodyTemperatureRepository(ApplicationDbContext context) : base(context)
    { 
    }

    public async Task<BodyTemperature> Add(BodyTemperature bodyTemperature)
    {
        if (await ExistsAsync(bodyTemperature.BodyTemperatureId))
        {
            throw new EntityDuplicationException(nameof(BodyTemperature), bodyTemperature.BodyTemperatureId);
        }

        return _context.BodyTemperatures
            .Add(bodyTemperature)
            .Entity;
    }

    public async Task<BodyTemperature?> GetAsync(string bodyTemperatureId)
    {
        return await _context.BodyTemperatures
            .Include(x => x.Person)
            .FirstOrDefaultAsync(x => x.BodyTemperatureId == bodyTemperatureId);
    }

    public async Task<BodyTemperature?> GetNewestAsync()
    {
        return await _context.BodyTemperatures
            .Include(x => x.Person)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task<List<BodyTemperature>> GetListByTimeQueryAsync(string personId, TimeQuery timeQuery)
    {
        return await _context.BodyTemperatures
            .Include(x => x.Person)
            .Where(x => x.Person.PersonId == personId
                && x.Timestamp >= timeQuery.StartTime
                && x.Timestamp <= timeQuery.EndTime)
            .OrderByDescending(x => x.Timestamp)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(string bodyTemperatureId)
    {
        return await _context.BodyTemperatures
            .AnyAsync(x => x.BodyTemperatureId == bodyTemperatureId);
    }
}
