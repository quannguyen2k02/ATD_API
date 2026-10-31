using Application.DTOs.LedStatistics;
using Application.DTOs.ResponseDTOs.LED;
using Application.IRepositories.LED;
using Domain.Enitties.LED;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LED
{
    public class LedResultRepository : GenericRepository<LedResult>, ILedResultRepository
    {
        private const string PassValue = "PASS";
        private const string FailValue = "FAIL";
        public LedResultRepository(ApplicationDbContext context) : base(context)
        {
        }

        private IQueryable<LedResult> BuildQuery(LedResultStatisticFilter filter)
        {
            var query = _context.LedResults.AsNoTracking().AsQueryable();

            // Lọc theo Line: dùng navigation property → EF sinh JOIN LedResults ↔ LED
            if (filter.LineId.HasValue)
                query = query.Where(r => r.LED != null && r.LED.LineId == filter.LineId.Value);

            // Lọc theo thiết bị
            if (filter.LEDId.HasValue)
                query = query.Where(r => r.LEDId == filter.LEDId.Value);

            // Lọc theo Model
            if (!string.IsNullOrWhiteSpace(filter.ModelName))
                query = query.Where(r => r.ModelName == filter.ModelName);

            // Lọc theo thời gian
            if (filter.FromDate.HasValue)
                query = query.Where(r => r.DateTime >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(r => r.DateTime <= filter.ToDate.Value);

            return query;
        }

        public async Task<LedResultStatisticDto> GetOverviewAsync(LedResultStatisticFilter filter)
        {
            var result = await BuildQuery(filter)
                .GroupBy(r => 1)
                .Select(g => new LedResultStatisticDto
                {
                    Total = g.Count(),
                    Pass = g.Sum(r => r.Result == PassValue ? 1 : 0),
                    Fail = g.Sum(r => r.Result == FailValue ? 1 : 0)
                })
                .FirstOrDefaultAsync();

            return result ?? new LedResultStatisticDto();
        }

        public async Task<List<ModelStatisticDto>> GetByModelAsync(LedResultStatisticFilter filter)
        {
            return await BuildQuery(filter)
                .GroupBy(r => r.ModelName)
                .Select(g => new ModelStatisticDto
                {
                    ModelName = g.Key,
                    Total = g.Count(),
                    Pass = g.Sum(r => r.Result == PassValue ? 1 : 0),
                    Fail = g.Sum(r => r.Result == FailValue ? 1 : 0)
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync();
        }

        public async Task<List<LineStatisticDto>> GetByLineAsync(LedResultStatisticFilter filter)
        {
            // Dùng navigation property: r.LED.LineId, r.LED.Line.Name
            return await BuildQuery(filter)
                .Where(r => r.LED != null)
                .GroupBy(r => new { r.LED!.LineId, LineName = r.LED.Line!.Name })
                .Select(g => new LineStatisticDto
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

        public async Task<List<DeviceStatisticDto>> GetByDeviceAsync(LedResultStatisticFilter filter)
        {
            return await BuildQuery(filter)
                .Where(r => r.LED != null)
                .GroupBy(r => new { r.LEDId, DeviceName = r.LED!.Name, r.LED.AssetId })
                .Select(g => new DeviceStatisticDto
                {
                    LEDId = g.Key.LEDId,
                    DeviceName = g.Key.DeviceName,
                    AssetId = g.Key.AssetId,
                    Total = g.Count(),
                    Pass = g.Sum(r => r.Result == PassValue ? 1 : 0),
                    Fail = g.Sum(r => r.Result == FailValue ? 1 : 0)
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync();
        }

        public async Task<List<DailyStatisticDto>> GetByDayAsync(LedResultStatisticFilter filter)
        {
            // 1. GroupBy trên server với anonymous type (EF Core dịch được)
            var groups = await BuildQuery(filter)
                .GroupBy(r => new
                {
                    r.DateTime.Year,
                    r.DateTime.Month,
                    r.DateTime.Day
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
                .Select(g => new DailyStatisticDto
                {
                    Date = new DateTime(g.Year, g.Month, g.Day),
                    Total = g.Total,
                    Pass = g.Pass,
                    Fail = g.Fail
                })
                .OrderBy(x => x.Date)
                .ToList();
        }

        public async Task<List<FailItemStatisticDto>> GetTopFailItemsAsync(LedResultStatisticFilter filter, int top = 10)
        {
            var query = BuildQuery(filter)
                .Where(r => r.Result == FailValue && r.FailItems != null && r.FailItems != "");

            var totalFail = await query.CountAsync();
            if (totalFail == 0) return new List<FailItemStatisticDto>();

            var groups = await query
                .GroupBy(r => r.FailItems)
                .Select(g => new { FailItems = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(top)
                .ToListAsync();

            return groups.Select(g => new FailItemStatisticDto
            {
                FailItems = g.FailItems,
                Count = g.Count,
                Rate = Math.Round((double)g.Count / totalFail * 100, 2)
            }).ToList();
        }
        public async Task<List<HourlyStatisticDto>> GetByHourAsync(LedResultStatisticFilter filter)
        {
            // 1. GroupBy theo giờ trong ngày trên server (EF Core dịch thành HOUR(DateTime) trong MySQL)
            var groups = await BuildQuery(filter)
                .GroupBy(r => r.DateTime.Hour)
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
                    return new HourlyStatisticDto
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

        public async Task<List<LedResultResponse>> GetBySNAsync(string sn)
        {
            if(string.IsNullOrEmpty(sn))
                return new List<LedResultResponse>();
            var trimmedSN = sn.Trim();
            var query = from r in _context.LedResults.AsNoTracking()
                        join led in _context.LEDs on r.LEDId equals led.Id
                        join line in _context.Lines on led.LineId equals line.Id into lineJoin
                        from line in lineJoin.DefaultIfEmpty()
                        where r.SN == trimmedSN
                        orderby r.Id descending
                        select new LedResultResponse
                        {
                            LEDId = r.LEDId,
                            SN = r.SN,
                            ModelName = r.ModelName,
                            Result = r.Result,
                            CT = r.CT,
                            FailItems = r.FailItems,
                            LineName = line != null ? line.Name : null,
                            DeviceName = led.Name,
                            DateTime = r.DateTime
                        };
            return await query.ToListAsync();
        }

        public async Task<List<LedResultResponse>> GetLedLogAsync(LedResultStatisticFilter filter)
        {
            var query = BuildQuery(filter)
                .Include(r => r.LED)
                .ThenInclude(led => led.Line)
                .OrderByDescending(r => r.DateTime)
                .Select(r => new LedResultResponse
                {
                    LEDId = r.LEDId,
                    SN = r.SN,
                    ModelName = r.ModelName,
                    Result = r.Result,
                    CT = r.CT,
                    FailItems = r.FailItems,
                    LineName = r.LED != null ? r.LED.Line!.Name : null,
                    DeviceName = r.LED != null ? r.LED.Name : null,
                    DateTime = r.DateTime
                }).Take(100);
            
            return await query.ToListAsync();

        }
    }
}
