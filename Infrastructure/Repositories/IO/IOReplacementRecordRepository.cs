using Application.DTOs.RequestDTOs.IO;
using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.IO
{
    public class IOReplacementRecordRepository : IIOReplacementRecordRepository
    {
        private readonly ApplicationDbContext _context;
        public IOReplacementRecordRepository(ApplicationDbContext context) => _context = context;

        public async Task<HashSet<(string, string, DateTime)>> GetExistingKeysAsync(
            IEnumerable<string> ioNames, CancellationToken ct)
        {
            var names = ioNames.Distinct().ToList();
            if (names.Count == 0) return new();

            var rows = await _context.ReplacementRecords
                .Where(x => names.Contains(x.IOName))
                .Select(x => new { x.IOName, x.Port, x.ReplaceDate })
                .ToListAsync(ct);

            return rows.Select(x => (x.IOName, x.Port, x.ReplaceDate)).ToHashSet();
        }

        public async Task AddRangeAsync(
            IEnumerable<ReplacementRecord> records, CancellationToken ct)
        {
            _context.ReplacementRecords.AddRange(records);
            try
            {
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex) when (IsUniqueViolation(ex))
            {
                _context.ChangeTracker.Clear();
                throw new DuplicateKeyException(ex);
            }
        }

        public async Task AddOneAsync(ReplacementRecord record, CancellationToken ct)
        {
            _context.ReplacementRecords.Add(record);
            try
            {
                await _context.SaveChangesAsync(ct);
            }
            catch (DbUpdateException ex) when (IsUniqueViolation(ex))
            {
                _context.ChangeTracker.Clear();
                throw new DuplicateKeyException(ex);
            }
        }

        private static bool IsUniqueViolation(DbUpdateException ex)
        {
            var inner = ex.InnerException;
            if (inner is null) return false;

            var numProp = inner.GetType().GetProperty("Number");
            if (numProp != null)
            {
                var n = Convert.ToInt32(numProp.GetValue(inner));
                if (n == 2601 || n == 2627 || n == 2067) return true;
            }

            var stateProp = inner.GetType().GetProperty("SqlState");
            if (stateProp?.GetValue(inner)?.ToString() == "23505") return true;

            return inner.Message.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase)
                || inner.Message.Contains("Duplicate", StringComparison.OrdinalIgnoreCase);
        }
    }
}
