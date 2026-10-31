using Application.DTOs.LedStatistics;
using Application.DTOs.ResponseDTOs.LED;
using Domain.Enitties.LED;

namespace Application.IRepositories.LED
{
    public interface ILedResultRepository : IGenericRepository<LedResult>
    {
        Task<LedResultStatisticDto> GetOverviewAsync(LedResultStatisticFilter filter);
        Task<List<ModelStatisticDto>> GetByModelAsync(LedResultStatisticFilter filter);
        Task<List<LineStatisticDto>> GetByLineAsync(LedResultStatisticFilter filter);
        Task<List<DeviceStatisticDto>> GetByDeviceAsync(LedResultStatisticFilter filter);
        Task<List<DailyStatisticDto>> GetByDayAsync(LedResultStatisticFilter filter);
        Task<List<HourlyStatisticDto>> GetByHourAsync(LedResultStatisticFilter filter);
        Task<List<LedResultResponse>> GetBySNAsync(string sn);
        Task<List<LedResultResponse>> GetLedLogAsync(LedResultStatisticFilter filter);
    }
}
