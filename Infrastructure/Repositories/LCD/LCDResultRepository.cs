using Application.Common;
using Application.IRepositories.LCD;
using Domain.Enitties.LCD;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.LCD
{
    public class LCDResultRepository : GenericRepository<LCDResult>, ILCDResultRepository
    {
        public LCDResultRepository(ApplicationDbContext context):base(context)
        {
        }
        public async Task AddRangeAsync(IEnumerable<LCDResult> results)
        {
            await _context.LCDResults.AddRangeAsync(results);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetExistingSNsAsync(IEnumerable<string> sns)
        {
            return await _context.LCDResults
                .Where(x => sns.Contains(x.SN))
                .Select(x => x.SN)
                .ToListAsync();
        }

        public Task<CursorPagedResult<LCDResult>> GetLCDResultsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            throw new NotImplementedException();
        }
    }
}
