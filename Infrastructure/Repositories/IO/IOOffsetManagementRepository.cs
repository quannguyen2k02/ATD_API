using Application.Common;
using Application.IRepositories;
using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.IO
{
    public class IOOffsetManagementRepository : GenericRepository<OffsetManagement>, IIOOffsetManagementRepository
    {
        public IOOffsetManagementRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CursorPagedResult<OffsetManagement>> GetOffsetManagement(int modelId, int? lastId = null, int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);

            // 1. Khai báo tường minh IQueryable
            IQueryable<OffsetManagement> baseQuery = _context.OffsetManagements
                .Include(x => x.Offsets)
                .Where(x => x.IOModelId == modelId);

            // 2. Sắp xếp giảm dần nên dùng dấu < để lấy các bản ghi cũ hơn
            if (lastId.HasValue)
            {
                baseQuery = baseQuery.Where(x => x.Id < lastId.Value);
            }

            // 3. Sắp xếp theo ngày tạo mới nhất
            var itemsWithExtra = await baseQuery
                .OrderByDescending(x => x.CreateDate) // Mới nhất trước
                .ThenByDescending(x => x.Id)          // Nếu trùng ngày thì ID lớn hơn đứng trước
                .Take(pageSize + 1)
                .ToListAsync();

            bool hasNextPage = itemsWithExtra.Count > pageSize;
            var items = hasNextPage
                ? itemsWithExtra.Take(pageSize).ToList()
                : itemsWithExtra;

            int? nextCursor = hasNextPage ? items.Last().Id : (int?)null;

            return new CursorPagedResult<OffsetManagement>
            {
                Items = items,
                NextCursor = nextCursor,
                HasNextPage = hasNextPage,
                PageSize = pageSize
            };
        }
    }
}
