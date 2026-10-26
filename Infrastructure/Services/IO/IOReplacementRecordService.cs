using Application.DTOs.RequestDTOs.IO;
using Application.IRepositories.IO;
using Application.IServices.IO;
using AutoMapper;
using Domain.Enitties.IO;

namespace Infrastructure.Services.IO
{
    public class IOReplacementRecordService : IIOReplacementRecordService
    {
        private readonly IIOReplacementRecordRepository _repo;
        private readonly IMapper _mapper;

        public IOReplacementRecordService(IIOReplacementRecordRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ImportResult> ImportAsync(List<ReplacementRecordRequest> rows, CancellationToken ct)
        {
            var result = new ImportResult { Total = rows.Count };

            // 1. Dedupe trong batch đầu vào
            var unique = rows
                .GroupBy(r => (r.IOName, r.Port, r.ReplaceDate))
                .Select(g => g.First())
                .ToList();
            result.Skipped += rows.Count - unique.Count;
            if (unique.Count == 0) return result;

            // 2. Lọc bỏ các dòng đã có trong DB
            var existing = await _repo.GetExistingKeysAsync(
                unique.Select(r => r.IOName), ct);

            var toInsert = unique
                .Where(r => !existing.Contains((r.IOName, r.Port, r.ReplaceDate)))
                .ToList();

            result.Skipped += unique.Count - toInsert.Count;
            if (toInsert.Count == 0) return result;
            var list = _mapper.Map<List<ReplacementRecord>>(toInsert).ToList();
            // 3. Insert batch — race condition thì fallback retry từng dòng
            try
            {
                await _repo.AddRangeAsync(list, ct);
                result.Inserted = list.Count;
            }
            catch (DuplicateKeyException ex)
            {

                foreach (var entity in toInsert)
                {
                    try
                    {
                        await _repo.AddOneAsync(_mapper.Map<ReplacementRecord>(entity), ct);
                        result.Inserted++;
                    }
                    catch (DuplicateKeyException)
                    {
                        result.Skipped++;
                    }
                }
            }

            return result;
        }
    }
}
