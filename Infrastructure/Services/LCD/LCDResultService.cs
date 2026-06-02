using Application.DTOs.RequestDTOs.LCD;
using Application.IRepositories.LCD;
using Application.IServices.LCD;
using AutoMapper;
using Domain.Enitties.LCD;

namespace Infrastructure.Services.LCD
{
    public class LCDResultService : ILCDResultService
    {
        private readonly ILCDResultRepository _lcdResultRepository;
        private readonly ILCDRepository _lcdRepository;
        private readonly IMapper _mapper;
        public LCDResultService(ILCDResultRepository lcdResultRepository, ILCDRepository lcdRepository, IMapper mapper)
        {
            _lcdResultRepository = lcdResultRepository;
            _lcdRepository = lcdRepository;
            _mapper = mapper;
        }
        public async Task<(int inserted, int skipped)> AddBatchAsync(IEnumerable<RequestLCDResult> batch)
        {
            var list = batch.ToList(); // ToList để tránh nhiều enumeration

            // Loại bỏ các bản ghi trùng SN trong cùng batch (giữ lại bản ghi đầu tiên theo thứ tự)
            var distinctBySN = list
                .GroupBy(x => x.SN)
                .Select(g => g.First()) // Giữ lại bản ghi đầu tiên cho mỗi SN
                .ToList();

            int skippedDueToDuplicateInBatch = list.Count - distinctBySN.Count;

            // Lấy các SN duy nhất để kiểm tra trong DB
            var sns = distinctBySN.Select(x => x.SN).Where(s => !string.IsNullOrEmpty(s)).Distinct();
            var existingSNs = await _lcdResultRepository.GetExistingSNsAsync(sns);

            var newRecords = distinctBySN.Where(x => !existingSNs.Contains(x.SN)).ToList();

            int skippedDueToExistingInDB = distinctBySN.Count - newRecords.Count;
            int totalSkipped = skippedDueToDuplicateInBatch + skippedDueToExistingInDB;

            if (newRecords.Any())
            {
                var entities = _mapper.Map<IEnumerable<LCDResult>>(newRecords);
                await _lcdResultRepository.AddRangeAsync(entities);
            }

            return (newRecords.Count, totalSkipped);
        }
    }
}
