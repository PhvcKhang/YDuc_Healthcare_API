using HealthCareApplication.Domains.Models;
using HealthCareApplication.Domains.Models.Queries;
using HealthCareApplication.Domains.Persistence.Contexts;
using HealthCareApplication.Domains.Persistence.Exceptions;
using HealthCareApplication.Domains.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApplication.Domains.Persistence.Repositories
{
    public class SpO2Repository : BaseRepository, ISpO2Repository
    {
        public SpO2Repository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<SpO2> Add(SpO2 spO2)
         {
                if (await ExistsAsync(spO2.SpO2Id))
                {
                    throw new EntityDuplicationException(nameof(BodyTemperature), spO2.SpO2Id);
                }

                return _context.SpO2s
                    .Add(spO2)
                    .Entity;
        }
        public async Task<bool> ExistsAsync(string bodyTemperatureId)
        {
            return await _context.BodyTemperatures
                .AnyAsync(x => x.BodyTemperatureId == bodyTemperatureId);
        }

        public async Task<List<SpO2>> GetAllAsync(string personId, TimeQuery timeQuery)
        {
            var spO2s = await _context.SpO2s
                .Where(x => x.Person.PersonId == personId 
                && x.Timestamp >= timeQuery.StartTime
                && x.Timestamp <= timeQuery.EndTime)
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
            return spO2s;
        }
    }
}
