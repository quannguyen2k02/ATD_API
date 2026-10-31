namespace Application.DTOs.LedStatistics
{
    public class LedResultStatisticFilter
    {
        public int? LineId { get; set; }
        public int? LEDId { get; set; }
        public string? ModelName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class LedResultStatisticDto
    {
        public int Total { get; set; }
        public int Pass { get; set; }
        public int Fail { get; set; }
        public double PassRate => Total == 0 ? 0 : Math.Round((double)Pass / Total * 100, 2);
        public double FailRate => Total == 0 ? 0 : Math.Round((double)Fail / Total * 100, 2);
    }

    public class ModelStatisticDto : LedResultStatisticDto
    {
        public string? ModelName { get; set; }
    }

    public class LineStatisticDto : LedResultStatisticDto
    {
        public int LineId { get; set; }
        public string? LineName { get; set; }
    }

    public class DeviceStatisticDto : LedResultStatisticDto
    {
        public int LEDId { get; set; }
        public string? DeviceName { get; set; }
        public string? AssetId { get; set; }
    }

    public class DailyStatisticDto : LedResultStatisticDto
    {
        public DateTime Date { get; set; }
    }

    public class FailItemStatisticDto
    {
        public string? FailItems { get; set; }
        public int Count { get; set; }
        public double Rate { get; set; }
    }
    public class HourlyStatisticDto : LedResultStatisticDto
    {
        public int Hour { get; set; }        // 0 → 23
        public string HourLabel { get; set; } = string.Empty; // "00:00-01:00"
    }
    public class LedResultReportDto
    {
        public LedResultStatisticDto? Overview { get; set; }
        public List<ModelStatisticDto>? ByModel { get; set; }
        public List<LineStatisticDto>? ByLine { get; set; }
        public List<DeviceStatisticDto>? ByDevice { get; set; }
        public List<DailyStatisticDto>? ByDay { get; set; }
        public List<HourlyStatisticDto>? ByHour { get; set; }  // <-- thêm mới

    }
}
