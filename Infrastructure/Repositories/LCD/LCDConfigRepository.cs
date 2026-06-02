using Application.Common;
using Application.IRepositories.LCD;
using Domain.Enitties.LCD;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LCD
{
    public class LCDConfigRepository : ILCDConfigRepository
    {
        private readonly ApplicationDbContext _context;
        public LCDConfigRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<LCDConfig> AddLCDConfigAsync(LCDConfig model)
        {
            await _context.LCDConfigs.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<LCDConfig> GetLCDConfigByIdAsync(int id)
        {
            var lcdConfig = await _context.LCDConfigs.FindAsync(id);
            return lcdConfig;
        }

        public async Task<CursorPagedResult<LCDConfig>> GetLCDConfigsByDeviceIdAsync(
    int deviceId,
    int? lastId = null,
    int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);

            var query = _context.LCDConfigs.Where(x => x.LCDId == deviceId);
            if (lastId.HasValue)
                query = query.Where(x => x.Id > lastId.Value);

            // Lấy danh sách Id (chỉ Id) để phân trang
            var ids = await query
                .OrderBy(x => x.Id)
                .Select(x => x.Id)
                .Take(pageSize + 1)
                .ToListAsync();

            bool hasNextPage = ids.Count > pageSize;
            var pagedIds = ids.Take(pageSize).ToList();

            var items = await _context.LCDConfigs
                .Where(x => pagedIds.Contains(x.Id))
                .OrderBy(x => x.Id)
                .ToListAsync();

            int? nextCursor = hasNextPage ? pagedIds.Last() : (int?)null;

            return new CursorPagedResult<LCDConfig>
            {
                Items = items,
                NextCursor = nextCursor,
                HasNextPage = hasNextPage,
                PageSize = pageSize
            };
        }
    }
}
