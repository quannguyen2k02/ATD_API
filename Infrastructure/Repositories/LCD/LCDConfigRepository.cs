using Application.Common;
using Application.IRepositories.LCD;
using Domain.Enitties.LCD;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LCD
{
    public class LCDConfigRepository : GenericRepository<LCDConfig>, ILCDConfigRepository
    {
        public LCDConfigRepository(ApplicationDbContext context):base(context)
        {
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
