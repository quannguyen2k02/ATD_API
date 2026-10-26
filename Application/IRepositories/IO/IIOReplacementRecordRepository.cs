using Domain.Enitties.IO;

namespace Application.IRepositories.IO
{
    public interface IIOReplacementRecordRepository
    {
        Task<HashSet<(string, string, DateTime)>> GetExistingKeysAsync(
        IEnumerable<string> ioNames, CancellationToken ct);

        Task AddRangeAsync(
            IEnumerable<ReplacementRecord> records, CancellationToken ct);

        Task AddOneAsync(ReplacementRecord record, CancellationToken ct);
    }
}
