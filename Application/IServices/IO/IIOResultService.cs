using Application.Common;
using Application.DTOs.IOStatitics;
using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;
using Domain.Enitties.IO;

namespace Application.IServices.IO
{
    public interface IIOResultService
    {
        Task<(int inserted, int skipped)> AddBatchAsync(IEnumerable<IOResultRequest> batch);
        Task<IOResultStatisticDto> GetOverviewAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.ModelStatisticDto>> GetByModelAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.ModelStatisticDto>> GetByModelGroupAsync(IOResultStatisticFilter filter);

        Task<List<DTOs.IOStatitics.LineStatisticDto>> GetByLineAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.DeviceStatisticDto>> GetByDeviceAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.DailyStatisticDto>> GetByDayAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.HourlyStatisticDto>> GetByHourAsync(IOResultStatisticFilter filter);
        Task<List<IOResultResponse>> GetBySNAsync(string sn);
        Task<List<IOResultResponse>> GetIOResultLogAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.FailItemStatisticDto>> GetFailItemStatisticsAsync(IOResultStatisticFilter filter);
        public Task<CursorPaged<IOResultResponse>> GetIOResultLogPagedAsync(
    IOResultStatisticFilter filter,
    int? lastId = null,
    int pageSize = 20);
    }
}
