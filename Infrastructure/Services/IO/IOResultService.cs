using Application.DTOs.RequestDTOs.IO;
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
    }
}
