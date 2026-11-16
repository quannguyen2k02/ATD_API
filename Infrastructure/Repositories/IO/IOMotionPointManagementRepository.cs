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

            // 1. Khai báo tường minh IQueryable
            IQueryable<MotionPointsManagement> baseQuery = _context.MotionPointsManagements
                .Include(x => x.MotionPoints)
                .Where(x => x.IOModelId == modelId);

            // 2. Vì sắp xếp giảm dần, dùng dấu < để lấy các bản ghi cũ hơn
            if (lastId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.Id < lastId.Value);
            }

            // 3. Sắp xếp mới nhất đến cũ nhất
            var itemsWithExtra = await baseQuery
                .OrderByDescending(x => x.CreateDate) // Sắp xếp theo ngày tạo mới nhất
                .ThenByDescending(x => x.Id)          // Nếu trùng ngày thì ID lớn hơn đứng trước
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