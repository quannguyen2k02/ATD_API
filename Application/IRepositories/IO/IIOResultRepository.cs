using Application.Common;
using Application.DTOs.IOStatitics;
using Application.DTOs.ResponseDTOs.IO;
using Domain.Enitties.IO;
namespace Application.IRepositories.IO
{
    public interface IIOResultRepository : IGenericRepository<IOResult>
    {
        public Task<IEnumerable<string>> GetExistingSNsAsync(IEnumerable<string> sns);
        public Task AddRangeAsync(IEnumerable<IOResult> results);
        Task<IOResultStatisticDto> GetOverviewAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.ModelStatisticDto>> GetByModelAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.ModelStatisticDto>> GetByModelGroupAsync(IOResultStatisticFilter filter);

        Task<List<DTOs.IOStatitics.LineStatisticDto>> GetByLineAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.DeviceStatisticDto>> GetByDeviceAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.DailyStatisticDto>> GetByDayAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.HourlyStatisticDto>> GetByHourAsync(IOResultStatisticFilter filter);
        Task<List<IOResult>> GetBySNAsync(string sn);
        Task<List<IOResult>> GetIOResultLogAsync(IOResultStatisticFilter filter);
        Task<List<DTOs.IOStatitics.FailItemStatisticDto>> GetFailItemStatisticsAsync(IOResultStatisticFilter filter);
        public Task<CursorPaged<IOResult>> GetIOResultLogPagedAsync(
    IOResultStatisticFilter filter,
    int? lastId = null,
    int pageSize = 20);
    }
}
