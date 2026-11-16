using Application.Common;
using Application.DTOs.IOStatitics;
using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.IO
{
    public class IOResultRepository : GenericRepository<IOResult>, IIOResultRepository
    {
        private const string PassValue = "PASS";
        private const string FailValue = "FAIL";
        public IOResultRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddRangeAsync(IEnumerable<IOResult> results)
        {
            await _context.IOResults.AddRangeAsync(results);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Application.DTOs.IOStatitics.DailyStatisticDto>> GetByDayAsync(IOResultStatisticFilter filter)
        {
            var groups = await BuildQuery(filter)
                .GroupBy(r => new
                {
                    r.CreateDate.Year,
                    r.CreateDate.Month,
                    r.CreateDate.Day
                })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    g.Key.Day,
                    Total = g.Count(),
                    Pass = g.Sum(r => r.Result == PassValue ? 1 : 0),
                    Fail = g.Sum(r => r.Result == FailValue ? 1 : 0)
                })
                .ToListAsync();

            // 2. Chuyển sang DTO trên client (sau khi đã có dữ liệu tổng hợp)
            return groups
                .Select(g => new Application.DTOs.IOStatitics.DailyStatisticDto
                {
                    Date = new DateTime(g.Year, g.Month, g.Day),
                    Total = g.Total,
                    Pass = g.Pass,
                    Fail = g.Fail
                })
                .OrderBy(x => x.Date)
                .ToList();
        }

        public async Task<List<Application.DTOs.IOStatitics.DeviceStatisticDto>> GetByDeviceAsync(IOResultStatisticFilter filter)
        {
            return await BuildQuery(filter)
                .Include(r => r.IO)
                .ThenInclude(io => io.Line)
                .Where(r => r.IO != null)
                .GroupBy(r => new { r.IOId, DeviceName = r.IO!.DeviceName, r.IO.AssetId, LineName = r.IO.Line != null ? r.IO.Line.Name : "Null", })
                .Select(g => new Application.DTOs.IOStatitics.DeviceStatisticDto
                {
                    IOId = g.Key.IOId,
                    DeviceName = g.Key.LineName + "-" + g.Key.DeviceName,
                    AssetId = g.Key.AssetId,
                    Total = g.Count(),
                    Pass = g.Sum(r => r.Result == PassValue ? 1 : 0),
                    Fail = g.Sum(r => r.Result == FailValue ? 1 : 0)
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync();
        }

        public async Task<List<Application.DTOs.IOStatitics.HourlyStatisticDto>> GetByHourAsync(IOResultStatisticFilter filter)
        {
            var groups = await BuildQuery(filter)
                .GroupBy(r => r.CreateDate.Hour)
                .Select(g => new
                {
                    Hour = g.Key,
                    Total = g.Count(),
                    Pass = g.Sum(r => r.Result == PassValue ? 1 : 0),
                    Fail = g.Sum(r => r.Result == FailValue ? 1 : 0)
                })
                .ToListAsync();

            // 2. Đảm bảo trả về đủ 24 giờ, giờ nào không có dữ liệu thì fill 0
            var dict = groups.ToDictionary(g => g.Hour);

            return Enumerable.Range(0, 24)
                .Select(h =>
                {
                    dict.TryGetValue(h, out var g);
                    return new Application.DTOs.IOStatitics.HourlyStatisticDto
                    {
                        Hour = h,
                        HourLabel = $"{h:D2}:00-{(h + 1) % 24:D2}:00",
                        Total = g?.Total ?? 0,
                        Pass = g?.Pass ?? 0,
                        Fail = g?.Fail ?? 0
                    };
                })
                .ToList();
        }

        public async Task<List<Application.DTOs.IOStatitics.LineStatisticDto>> GetByLineAsync(IOResultStatisticFilter filter)
        {
            return await BuildQuery(filter)
                .Where(r => r.IO != null)
                .GroupBy(r => new { r.IO!.LineId, LineName = r.IO.Line!.Name })
                .Select(g => new Application.DTOs.IOStatitics.LineStatisticDto
                {
                    LineId = g.Key.LineId,
                    LineName = g.Key.LineName,
                    Total = g.Count(),
                    Pass = g.Sum(r => r.Result == PassValue ? 1 : 0),
                    Fail = g.Sum(r => r.Result == FailValue ? 1 : 0)
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync();
        }

        public async Task<List<Application.DTOs.IOStatitics.ModelStatisticDto>> GetByModelAsync(IOResultStatisticFilter filter)
        {
            return await BuildQuery(filter)
                .GroupBy(r => r.Model)
                .Select(g => new Application.DTOs.IOStatitics.ModelStatisticDto
                {
                    ModelName = g.Key,
                    Total = g.Count(),
                    Pass = g.Sum(r => r.Result == PassValue ? 1 : 0),
                    Fail = g.Sum(r => r.Result == FailValue ? 1 : 0)
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync();
        }

        public Task<List<Application.DTOs.IOStatitics.ModelStatisticDto>> GetByModelGroupAsync(IOResultStatisticFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IOResult>> GetBySNAsync(string sn)
        {
            if (string.IsNullOrWhiteSpace(sn))
                return new List<IOResult>();

            var snList = sn.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (!snList.Any())
                return new List<IOResult>();

            var query = _context.IOResults
                .AsNoTracking()
                // 👇 Thêm Include để lấy thông tin Device và Line
                .Include(r => r.IO)
                    .ThenInclude(io => io.Line)
                .Where(r => snList.Contains(r.SN))
                .OrderByDescending(r => r.Id);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetExistingSNsAsync(IEnumerable<string> sns)
        {
            return await _context.IOResults
                .Where(x => sns.Contains(x.SN))
                .Select(x => x.SN)
                .ToListAsync();
        }

        public Task<List<Application.DTOs.IOStatitics.FailItemStatisticDto>> GetFailItemStatisticsAsync(IOResultStatisticFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IOResult>> GetIOResultLogAsync(IOResultStatisticFilter filter)
        {
            var query = BuildQuery(filter)
                .Include(r => r.IO)
                .ThenInclude(io => io.Line)
                .OrderByDescending(r => r.CreateDate).Take(100);

            var ioResult = await query.ToListAsync();

            return ioResult;
        }

        public async Task<CursorPaged<IOResult>> GetIOResultLogPagedAsync(IOResultStatisticFilter filter, int? lastId = null, int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);

            // 1. Lấy Query từ Filter
            var query = BuildQuery(filter).AsNoTracking();

            // 2. Gắn điều kiện Cursor
            if (lastId.HasValue)
            {
                query = query.Where(r => r.Id < lastId.Value);
            }

            // 3. Chiếu dữ liệu và phân trang
            var itemsWithExtra = await query
                .OrderByDescending(r => r.Id)
                .Include(r => r.IO)
                .ThenInclude(io => io.Line)
                .Take(pageSize + 1)
                .ToListAsync();

            // 4. Xử lý kết quả
            bool hasNextPage = itemsWithExtra.Count > pageSize;
            var items = hasNextPage ? itemsWithExtra.Take(pageSize).ToList() : itemsWithExtra;

            int? nextCursor = hasNextPage && items.Any() ? items.Last().Id : null;

            return new CursorPaged<IOResult>
            {
                Items = items,
                NextCursor = nextCursor,
                HasNextPage = hasNextPage,
                PageSize = pageSize
            };
        }
        public Task<IOResultStatisticDto> GetOverviewAsync(IOResultStatisticFilter filter)
        {
            throw new NotImplementedException();
        }
        private IQueryable<IOResult> BuildQuery(IOResultStatisticFilter filter)
        {
            var query = _context.IOResults.AsNoTracking().AsQueryable();

            // Lọc theo Line: dùng navigation property → EF sinh JOIN IOResults ↔ LED
            if (filter.LineId.HasValue)
                query = query.Where(r => r.IO != null && r.IO.LineId == filter.LineId.Value);

            // Lọc theo thiết bị
            if (filter.IOId.HasValue)
                query = query.Where(r => r.IOId == filter.IOId.Value);

            // Lọc theo Model
            if (!string.IsNullOrWhiteSpace(filter.ModelName))
                query = query.Where(r => r.Model == filter.ModelName);

            // Lọc theo thời gian
            if (filter.FromDate.HasValue)
                query = query.Where(r => r.CreateDate >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(r => r.CreateDate <= filter.ToDate.Value);

            return query;
        }
    }
}