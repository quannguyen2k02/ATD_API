using Application.Common;
using Application.IRepository.LED;
using Domain.Entities.LED;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LED
{
    public class LedModelRepository : GenericRepository<LedModel> , ILedModelRepository
    {
        public LedModelRepository(ApplicationDbContext context):base(context)
        {
        }

        public async Task<CursorPagedResult<LedModel>> GetLedModelAsync(int deviceId, string model, string kb, string fp, int? lastId = null, int pageSize = 20)
        {
            // Giới hạn pageSize
            pageSize = Math.Clamp(pageSize, 1, 100);

            var baseQuery = _context.LEDModels
                .Include(x => x.LEDModelConfigs)
                .Include(x => x.Cameras)
                    .ThenInclude(c => c.LedStatuses)
                        .ThenInclude(c => c.Jobs)
                .Where(x => x.KB == kb && x.FP == fp && x.LedId == deviceId && x.Name.ToLower() == model.ToLower());

            if (lastId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.Id > lastId.Value);
            }

            var itemsWithExtra = await baseQuery
                .OrderBy(x => x.Id)
                .Take(pageSize + 1)
                .ToListAsync();

            bool hasNextPage = itemsWithExtra.Count > pageSize;
            var items = hasNextPage
                ? itemsWithExtra.Take(pageSize).ToList()
                : itemsWithExtra;

            int? nextCursor = hasNextPage ? items.Last().Id : (int?)null;

            return new CursorPagedResult<LedModel>
            {
                Items = items,
                NextCursor = nextCursor,
                HasNextPage = hasNextPage,
                PageSize = pageSize
            };
        }

        public async Task<CursorPagedResult<LedModel>> GetLedModelsByDeviceIdAsync(int id, int? lastId = null, int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);

            // Lấy Id lớn nhất mỗi nhóm
            var latestIdsQuery = _context.LEDModels
                .Where(x => x.LedId == id)
                .GroupBy(x => new { x.Name, x.KB, x.FP })
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

            var items = await _context.LEDModels
                .Where(x => pagedIds.Contains(x.Id))
                .OrderBy(x => x.Id)
                .ToListAsync();

            int? nextCursor = hasNextPage ? pagedIds.Last() : (int?)null;

            return new CursorPagedResult<LedModel>
            {
                Items = items,
                NextCursor = nextCursor,
                HasNextPage = hasNextPage,
                PageSize = pageSize
            };
        }

    }
}
