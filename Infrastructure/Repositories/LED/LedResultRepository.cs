using Application.Common;
using Application.IRepositories.LED;
using Domain.Enitties.LCD;
using Domain.Enitties.LED;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LED
{
    public class LedResultRepository : ILedResultRepository
    {
        private readonly ApplicationDbContext _context;
        public LedResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddBatchLedResult(IEnumerable<LedResult> ledResults)
        {
            await _context.LedResults.AddRangeAsync(ledResults);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetExistingSNsAsync(IEnumerable<string> sns)
        {
            return await _context.LedResults
                .Where(x => sns.Contains(x.SN))
                .Select(x => x.SN)
                .ToListAsync();
        }

        public Task<CursorPagedResult<LCDResult>> GetLedResultsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            throw new NotImplementedException();
        }
    }
}
