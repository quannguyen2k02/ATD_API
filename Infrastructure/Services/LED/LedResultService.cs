using Application.DTOs.LedStatistics;
using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs.LED;
using Application.IRepositories.LED;
using Application.IRepository.LED;
using Application.IServices.LED;
using AutoMapper;
using Domain.Enitties.LED;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Infrastructure.Repositories.IO;

namespace Infrastructure.Services.LED
{
    public class LedResultService : ILedResultService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILedResultRepository _ledResultRepository;
        private readonly ILedRepository _ledRepository;
        private readonly IMapper _mapper;
        public LedResultService(ApplicationDbContext context, ILedResultRepository ledResultRepository, ILedRepository ledRepository, IMapper mapper)
        {
            _context = context;
            _ledResultRepository = ledResultRepository;
            _ledRepository = ledRepository;
            _mapper = mapper;
        }
        public async Task<LedResultResponse> AddnewLedResult(LedResultRequest ledResultRequest)
        {
            var led = await _ledRepository.GetByIDAsync(ledResultRequest.LEDId);
            if(led == null)
            {
                throw new NotFoundException($"LED with ID {ledResultRequest.LEDId} not found.");
            }
            var ledResult = _mapper.Map<LedResult>(ledResultRequest);
            var result = await _ledResultRepository.Add(ledResult);
            return _mapper.Map<LedResultResponse>(result);
        }

        public Task<LedResultStatisticDto> GetOverviewAsync(LedResultStatisticFilter filter)
            => _ledResultRepository.GetOverviewAsync(NormalizeFilter(filter));

        public Task<List<ModelStatisticDto>> GetByModelAsync(LedResultStatisticFilter filter)
            => _ledResultRepository.GetByModelAsync(NormalizeFilter(filter));

        public Task<List<LineStatisticDto>> GetByLineAsync(LedResultStatisticFilter filter)
            => _ledResultRepository.GetByLineAsync(NormalizeFilter(filter));

        public Task<List<DeviceStatisticDto>> GetByDeviceAsync(LedResultStatisticFilter filter)
            => _ledResultRepository.GetByDeviceAsync(NormalizeFilter(filter));

        public Task<List<DailyStatisticDto>> GetByDayAsync(LedResultStatisticFilter filter)
            => _ledResultRepository.GetByDayAsync(NormalizeFilter(filter));
        public Task<List<HourlyStatisticDto>> GetByHourAsync(LedResultStatisticFilter filter)
    => _ledResultRepository.GetByHourAsync(NormalizeFilter(filter));

        public async Task<LedResultReportDto> GetFullReportAsync(LedResultStatisticFilter filter)
        {
            var normalized = NormalizeFilter(filter);

            // Chạy song song để tăng tốc (mỗi query chạy trên connection riêng của DbContext scope)
            var overviewTask = _ledResultRepository.GetOverviewAsync(normalized);
            var byModelTask = _ledResultRepository.GetByModelAsync(normalized);
            var byLineTask = _ledResultRepository.GetByLineAsync(normalized);
            var byDeviceTask = _ledResultRepository.GetByDeviceAsync(normalized);
            var byDayTask = _ledResultRepository.GetByDayAsync(normalized);

            await Task.WhenAll(overviewTask, byModelTask, byLineTask, byDeviceTask, byDayTask);

            return new LedResultReportDto
            {
                Overview = await overviewTask,
                ByModel = await byModelTask,
                ByLine = await byLineTask,
                ByDevice = await byDeviceTask,
                ByDay = await byDayTask,
            };
        }

        /// <summary>
        /// Chuẩn hoá filter: nếu FromDate và ToDate cùng ngày → mở rộng ToDate đến cuối ngày.
        /// </summary>
        private static LedResultStatisticFilter NormalizeFilter(LedResultStatisticFilter filter)
        {
            if (filter.FromDate.HasValue && filter.ToDate.HasValue
                && filter.FromDate.Value.Date == filter.ToDate.Value.Date)
            {
                filter.ToDate = filter.ToDate.Value.Date.AddDays(1).AddTicks(-1);
            }
            return filter;
        }

        public Task<List<LedResultResponse>> GetBySNAsync(string sn)
            => _ledResultRepository.GetBySNAsync(sn);

        public async Task<List<LedResultResponse>> GetLedLogAsync(LedResultStatisticFilter filter)
        {
            return await _ledResultRepository.GetLedLogAsync(filter);
        }
    }
}