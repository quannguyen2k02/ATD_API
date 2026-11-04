using Application.Common;
using Application.IRepositories.LED;
using Domain.Entities.LED;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LED
{
    public class LedConfigRepository :GenericRepository<LedConfig>,  ILedConfigRepository
    {
        public LedConfigRepository(ApplicationDbContext context) :base(context)
        {
        }


        public async Task<CursorPagedResult<LedConfig>> GetLedConfigByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            // Giới hạn pageSize hợp lý
            pageSize = Math.Clamp(pageSize, 1, 100);

            // 1. Khai báo tường minh IQueryable
            IQueryable<LedConfig> baseQuery = _context.LedConfigs.Where(x => x.LedId == deviceId);

            // 2. Vì sắp xếp giảm dần, dùng dấu < để lấy các bản ghi cũ hơn
            if (lastId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.ID < lastId.Value);
            }

            // 3. Sắp xếp mới nhất đến cũ nhất và lấy dữ liệu
            var itemsWithExtra = await baseQuery
                .OrderByDescending(x => x.CreateDate) // Mới nhất trước
                .ThenByDescending(x => x.ID)          // Nếu trùng CreateDate thì ID lớn hơn đứng trước
                .Take(pageSize + 1)
                .AsNoTracking()
                .ToListAsync();

            bool hasNextPage = itemsWithExtra.Count > pageSize;
            var items = hasNextPage ? itemsWithExtra.Take(pageSize).ToList() : itemsWithExtra;

            int? nextCursor = hasNextPage ? items.Last().ID : (int?)null;

            return new CursorPagedResult<LedConfig>
            {
                Items = items,
                NextCursor = nextCursor,
                HasNextPage = hasNextPage,
                PageSize = pageSize
            };
        }

    }
}
