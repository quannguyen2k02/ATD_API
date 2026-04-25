using Application.Common;
using Application.IRepositories.LED;
using Domain.Entities.LED;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LED
{
    public class LedConfigRepository : ILedConfigRepository
    {
        private readonly ApplicationDbContext _context;
        public LedConfigRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<LedConfig> AddLedModelAsync(LedConfig ledConfig)
        {
            ledConfig.CreateDate = DateTime.Now;
            ledConfig.ModifiedDate = DateTime.Now;
            await _context.LedConfigs.AddAsync(ledConfig);
            await _context.SaveChangesAsync();
            return ledConfig;

        }

        public async Task<PagedResult<LedConfig>> GetLedConfigByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            var baseQuery = _context.LedConfigs.Where(x => x.LedId == deviceId);
            if (lastId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.ID > lastId.Value);
            }
            var totalCount = await _context.LEDModels
                .Where(x => x.LedId == deviceId)
                .CountAsync();
            var items = await baseQuery
                .OrderBy(x => x.ID)
                .Take(Math.Max(pageSize, 1))
                .ToListAsync();
            return new PagedResult<LedConfig>
            {
                Items = items,
                PageNumber = lastId ?? 0, // reuse PageNumber field to carry lastId for client; not ideal but keeps signature
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

    }
}
