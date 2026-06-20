
using Application.Common;
using Application.IRepositories.LCD;
using Domain.Enitties.LCD;
using Domain.Entities.LED;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LCD
{
    public class LCDModelRepository : GenericRepository<LCDModel>, ILCDModelRepository
    {
        public LCDModelRepository(ApplicationDbContext context):base(context)
        {
        }


        public async Task<CursorPagedResult<LCDModel>> GetLCDModelsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);

            // Lấy Id lớn nhất mỗi nhóm
            var latestIdsQuery = _context.LCDModels
                .Where(x => x.LCDId == deviceId)
                .GroupBy(x => new { x.ModelName })
                .Select(g => g.Max(x => x.Id));

            if (lastId.HasValue)
            {
                latestIdsQuery = latestIdsQuery.Where(x => x > lastId.Value);
            }

            // Lấy thêm 1 để kiểm tra còn trang tiếp theo không
            var idsWithExtra = await latestIdsQuery
                .OrderBy(x => x)
                .Take(pageSize + 1)
                .ToListAsync();

            bool hasNextPage = idsWithExtra.Count > pageSize;
            var pagedIds = idsWithExtra.Take(pageSize).ToList();

            var items = await _context.LCDModels
                .Where(x => pagedIds.Contains(x.Id))
                .OrderBy(x => x.Id)
                .ToListAsync();

            int? nextCursor = hasNextPage ? pagedIds.Last() : (int?)null;

            return new CursorPagedResult<LCDModel>
            {
                Items = items,
                NextCursor = nextCursor,
                HasNextPage = hasNextPage,
                PageSize = pageSize
            };

        }

        public async Task<CursorPagedResult<LCDModel>> GetLCDModelsByModelNameAsync(string modelName, int deviceId, int? lastId = null, int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);
            var baseQuery = _context.LCDModels.Where(x => x.LCDId == deviceId && x.ModelName == modelName);
            if (lastId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.Id > lastId.Value);
            }
            var itemsWithExtra = await baseQuery
                .OrderBy(x => x.Id)
                .Take(pageSize + 1)
                .ToListAsync();

            bool hasNextPage = itemsWithExtra.Count > pageSize;
            var items = hasNextPage ? itemsWithExtra.Take(pageSize).ToList() : itemsWithExtra;
            int? nextCursor = hasNextPage ? items.Last().Id : (int?)null;
            return new CursorPagedResult<LCDModel>
            {
                Items = items,
                NextCursor = nextCursor,
                HasNextPage = hasNextPage,
                PageSize = pageSize
            };
        }
    }
}
