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

            var baseQuery = _context.LedConfigs.Where(x => x.LedId == deviceId);
            if (lastId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.ID > lastId.Value);
            }

            // Lấy thêm 1 bản ghi để xác định còn trang sau không
            var itemsWithExtra = await baseQuery
                .OrderBy(x => x.ID)
                .Take(pageSize + 1)
                .ToListAsync();

            bool hasNextPage = itemsWithExtra.Count > pageSize;
            var items = hasNextPage ? itemsWithExtra.Take(pageSize).ToList() : itemsWithExtra;

            int? nextCursor = hasNextPage ? items.Last().ID : (int?)null;

            // Optional: tính totalCount nếu cần, nhưng có thể bỏ qua để tối ưu
            // var totalCount = await _context.LedConfigs.CountAsync(x => x.LedId == deviceId);

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
