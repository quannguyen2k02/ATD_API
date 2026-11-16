using Application.Common;
using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.IO
{
    public class IOConfigManagementRepository : GenericRepository<IOConfigManagement>, IIOConfigManagementRepository
    {
        public IOConfigManagementRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CursorPagedResult<IOConfigManagement>> GetIOConfigManagement(int modelId, int? lastId = null, int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);

            // 1. Khai báo tường minh IQueryable
            IQueryable<IOConfigManagement> baseQuery = _context.IOConfigManagements
                .Include(x => x.IOConfigs)
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

            return new CursorPagedResult<IOConfigManagement>
            {
                Items = items,
                NextCursor = nextCursor,
                HasNextPage = hasNextPage,
                PageSize = pageSize
            };
        }
    }
}
