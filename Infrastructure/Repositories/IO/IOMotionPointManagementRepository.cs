using Application.Common;
using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.IO
{
    public class IOMotionPointManagementRepository : GenericRepository<MotionPointsManagement>, IIOMotionPointsManagementRepository
    {
        public IOMotionPointManagementRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CursorPagedResult<MotionPointsManagement>> GetIOMotionPoints(int modelId, int? lastId = null, int pageSize = 20)
        {
            // Giới hạn pageSize
            pageSize = Math.Clamp(pageSize, 1, 100);

            var baseQuery = _context.MotionPointsManagements
                .Include(x => x.MotionPoints)
                .Where(x => x.IOModelId == modelId);

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

            return new CursorPagedResult<MotionPointsManagement>
            {
                Items = items,
                NextCursor = nextCursor,
                HasNextPage = hasNextPage,
                PageSize = pageSize
            };
        }
    }
}