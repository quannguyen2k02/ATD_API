using Application.Common;
using Application.DTOs.ResponseDTOs.LCD;
using Application.IRepositories.LCD;
using AutoMapper;

namespace Infrastructure.Services.LCD
{
    public class LCDResultDemoService : ILCDResultDemoService
    {
        private readonly ILCDResultDemoRepository _lcdResultDemoRepository;
        private readonly IMapper _mapper;
        public LCDResultDemoService(ILCDResultDemoRepository lcdResultDemoRepository, IMapper mapper)
        {
            _lcdResultDemoRepository = lcdResultDemoRepository;
            _mapper = mapper;
        }
        public async Task<CursorPaged<ResponseLCDResult>> GetLCDResultDemoAsync(
    DateTime? fromDate,
    DateTime? toDate,
    string? modelName,
    int deviceId,
    int? lastId = null,
    int pageSize = 20)
        {
            // Xử lý khoảng thời gian: nếu fromDate và toDate cùng ngày -> mở rộng toDate đến cuối ngày
            DateTime? effectiveFromDate = fromDate;
            DateTime? effectiveToDate = toDate;

            if (fromDate.HasValue && toDate.HasValue && fromDate.Value.Date == toDate.Value.Date)
            {
                // Cùng một ngày: lấy từ đầu ngày đến cuối ngày
                effectiveFromDate = fromDate.Value.Date;                     // 00:00:00
                effectiveToDate = fromDate.Value.Date.AddDays(1).AddTicks(-1); // 23:59:59.999
            }

            var result = await _lcdResultDemoRepository.GetPagedCursorAsync(
                orderBy: x => x.Id,
                cursor: lastId,
                pageSize: pageSize,
                predicate: x => x.LCDId == deviceId
                    && (modelName == null || x.ModelName == modelName)
                    && (!effectiveFromDate.HasValue || x.CreateDate >= effectiveFromDate.Value)
                    && (!effectiveToDate.HasValue || x.CreateDate <= effectiveToDate.Value)
            );

            var dtos = _mapper.Map<List<ResponseLCDResult>>(result.Items);
            return new CursorPaged<ResponseLCDResult>
            {
                Items = dtos,
                NextCursor = result.NextCursor,
                HasNextPage = result.HasNextPage,
                PageSize = result.PageSize
            };
        }
    }
}
