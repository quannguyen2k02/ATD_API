using Application.Common;
using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs.LED;
using Application.IRepositories.LED;
using Application.IRepository.LED;
using Application.IServices.LED;
using AutoMapper;
using Domain.Enitties.LED;
using Infrastructure.Exceptions;

namespace Infrastructure.Services.LED
{
    public class LedResultService : ILedResultService
    {
        private readonly ILedResultRepository _ledResultRepository;
        private readonly IMapper _mapper;
        private readonly ILedRepository _ledRepository;
        public LedResultService(ILedResultRepository ledResultRepository, IMapper mapper, ILedRepository ledRepository)
        {
            _ledResultRepository = ledResultRepository;
            _mapper = mapper;
            _ledRepository = ledRepository;
        }
        public async Task<(int inserted, int skipped)> AddBatchLedResultAsync(IEnumerable<LedResultRequest> batch)
        {
            var list = batch.ToList(); // ToList để tránh nhiều enumeration
            var device = _ledRepository.GetDeviceNameByIdAsync(list.FirstOrDefault()?.LEDId ?? 0).Result;
            if(device == null)
            {
                throw new NotFoundException($"LED device with ID {list.FirstOrDefault()?.LEDId} not found.");
            }
            // Loại bỏ các bản ghi trùng SN trong cùng batch (giữ lại bản ghi đầu tiên theo thứ tự)
            var distinctBySN = list
                .GroupBy(x=>x.SN)
                .Select(g => g.First()) // Giữ lại bản ghi đầu tiên cho mỗi SN
                .ToList();

            int skippedDueToDuplicateInBatch = list.Count - distinctBySN.Count;

            // Lấy các SN duy nhất để kiểm tra trong DB
            var sns = distinctBySN.Select(x => x.SN).Where(s => !string.IsNullOrEmpty(s)).Distinct();
            var existingSNs = await _ledResultRepository.GetExistingSNsAsync(sns);

            var newRecords = distinctBySN.Where(x => !existingSNs.Contains(x.SN)).ToList();

            int skippedDueToExistingInDB = distinctBySN.Count - newRecords.Count;
            int totalSkipped = skippedDueToDuplicateInBatch + skippedDueToExistingInDB;

            if (newRecords.Any())
            {
                var entities = _mapper.Map<IEnumerable<LedResult>>(newRecords);
                await _ledResultRepository.AddBatchLedResult(entities);
            }

            return (newRecords.Count, totalSkipped);
        }

        public async Task<IEnumerable<string>> GetExistingSNsAsync(IEnumerable<string> sns)
        {
            throw new NotImplementedException();
        }

        public async Task<CursorPagedResult<LedResultResponse>> GetLedResultsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            throw new NotImplementedException();
        }
    }
}
