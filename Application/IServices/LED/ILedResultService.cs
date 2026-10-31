using Application.DTOs.LedStatistics;
using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs.LED;

namespace Application.IServices.LED
{
    public interface ILedResultService
    {
        public Task<LedResultResponse> AddnewLedResult(LedResultRequest ledResultRequest);
        Task<LedResultStatisticDto> GetOverviewAsync(LedResultStatisticFilter filter);
        Task<List<ModelStatisticDto>> GetByModelAsync(LedResultStatisticFilter filter);
        Task<List<LineStatisticDto>> GetByLineAsync(LedResultStatisticFilter filter);
        Task<List<DeviceStatisticDto>> GetByDeviceAsync(LedResultStatisticFilter filter);
        Task<List<DailyStatisticDto>> GetByDayAsync(LedResultStatisticFilter filter);
        Task<LedResultReportDto> GetFullReportAsync(LedResultStatisticFilter filter);
        Task<List<HourlyStatisticDto>> GetByHourAsync(LedResultStatisticFilter filter);
        Task<List<LedResultResponse>> GetBySNAsync(string sn);
        Task<List<LedResultResponse>> GetLedLogAsync(LedResultStatisticFilter filter);
    }
}
