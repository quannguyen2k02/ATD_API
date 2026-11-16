using Application.Common;
using Application.DTOs.IOStatitics;
using Application.DTOs.LedStatistics;
using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;
using Domain.Enitties.IO;
using Domain.Enitties.LCD;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.IO
{
    public class IOResultService : IIOResultService
    {
        private readonly IIOResultRepository _ioResultRepository;
        private readonly IIORepository _ioRepository;
        private readonly IMapper _mapper;
        public IOResultService(IIOResultRepository ioResultRepository, IIORepository ioRepository, IMapper mapper)
        {
            _ioResultRepository = ioResultRepository;
            _ioRepository = ioRepository;
            _mapper = mapper;
        }
        public async Task<(int inserted, int skipped)> AddBatchAsync(IEnumerable<IOResultRequest> batch)
        {
            var list = batch.ToList(); // ToList để tránh nhiều enumeration
            var device = _ioRepository.GetByIDAsync(list.FirstOrDefault()?.IOId ?? 0).Result;
            if (device == null)
            {
                throw new NotFoundException($"IO device with ID {list.FirstOrDefault()?.IOId} not found.");
            }
            // Loại bỏ các bản ghi trùng SN trong cùng batch (giữ lại bản ghi đầu tiên theo thứ tự)
            var distinctBySN = list
                .GroupBy(x => x.SN)
                .Select(g => g.First()) // Giữ lại bản ghi đầu tiên cho mỗi SN
                .ToList();

            int skippedDueToDuplicateInBatch = list.Count - distinctBySN.Count;

            // Lấy các SN duy nhất để kiểm tra trong DB
            var sns = distinctBySN.Select(x => x.SN).Where(s => !string.IsNullOrEmpty(s)).Distinct();
            var existingSNs = await _ioResultRepository.GetExistingSNsAsync(sns);

            var newRecords = distinctBySN.Where(x => !existingSNs.Contains(x.SN)).ToList();

            int skippedDueToExistingInDB = distinctBySN.Count - newRecords.Count;
            int totalSkipped = skippedDueToDuplicateInBatch + skippedDueToExistingInDB;

            if (newRecords.Any())
            {
                var entities = _mapper.Map<IEnumerable<IOResult>>(newRecords);
                await _ioResultRepository.AddRangeAsync(entities);
            }

            return (newRecords.Count, totalSkipped);
        }

        public Task<List<Application.DTOs.IOStatitics.DailyStatisticDto>> GetByDayAsync(IOResultStatisticFilter filter)
         => _ioResultRepository.GetByDayAsync(NormalizeFilter(filter));

        public Task<List<Application.DTOs.IOStatitics.DeviceStatisticDto>> GetByDeviceAsync(IOResultStatisticFilter filter)
        => _ioResultRepository.GetByDeviceAsync(NormalizeFilter(filter));

        public Task<List<Application.DTOs.IOStatitics.HourlyStatisticDto>> GetByHourAsync(IOResultStatisticFilter filter)
        => _ioResultRepository.GetByHourAsync(NormalizeFilter(filter));

        public Task<List<Application.DTOs.IOStatitics.LineStatisticDto>> GetByLineAsync(IOResultStatisticFilter filter)
        => _ioResultRepository.GetByLineAsync(NormalizeFilter(filter));

        public Task<List<Application.DTOs.IOStatitics.ModelStatisticDto>> GetByModelAsync(IOResultStatisticFilter filter)
        => _ioResultRepository.GetByModelAsync(NormalizeFilter(filter));

        public Task<List<Application.DTOs.IOStatitics.ModelStatisticDto>> GetByModelGroupAsync(IOResultStatisticFilter filter)
        => _ioResultRepository.GetByModelGroupAsync(NormalizeFilter(filter));

        public async Task<List<IOResultResponse>> GetBySNAsync(string sn)
        {
            var result = await _ioResultRepository.GetBySNAsync(sn.Trim());
            return _mapper.Map<List<IOResultResponse>>(result);
        }

        public Task<List<Application.DTOs.IOStatitics.FailItemStatisticDto>> GetFailItemStatisticsAsync(IOResultStatisticFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IOResultResponse>> GetIOResultLogAsync(IOResultStatisticFilter filter)
        {
            var result = await _ioResultRepository.GetIOResultLogAsync(NormalizeFilter(filter));
            return _mapper.Map<List<IOResultResponse>>(result);
        }

        public async Task<CursorPaged<IOResultResponse>> GetIOResultLogPagedAsync(IOResultStatisticFilter filter, int? lastId = null, int pageSize = 20)
        {
            var pagedEntities = await _ioResultRepository.GetIOResultLogPagedAsync(filter, lastId, pageSize);

            return new CursorPaged<IOResultResponse>
            {
                Items = _mapper.Map<List<IOResultResponse>>(pagedEntities.Items),
                NextCursor = pagedEntities.NextCursor,
                HasNextPage = pagedEntities.HasNextPage,
                PageSize = pagedEntities.PageSize
            };
        }

        public Task<IOResultStatisticDto> GetOverviewAsync(IOResultStatisticFilter filter)
        {
            throw new NotImplementedException();
        }
        private static IOResultStatisticFilter NormalizeFilter(IOResultStatisticFilter filter)
        {
            if (filter.FromDate.HasValue && filter.ToDate.HasValue
                && filter.FromDate.Value.Date == filter.ToDate.Value.Date)
            {
                filter.ToDate = filter.ToDate.Value.Date.AddDays(1).AddTicks(-1);
            }
            return filter;
        }
    }
}
