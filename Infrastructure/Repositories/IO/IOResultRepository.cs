using Application.Common;
using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.IO
{
    public class IOResultRepository : GenericRepository<IOResult>, IIOResultRepository
    {
        public IOResultRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddRangeAsync(IEnumerable<IOResult> results)
        {
            await _context.IOResults.AddRangeAsync(results);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetExistingSNsAsync(IEnumerable<string> sns)
        {
            return await _context.IOResults
                .Where(x => sns.Contains(x.SN))
                .Select(x => x.SN)
                .ToListAsync();
        }

        public async Task<CursorPagedResult<IOResult>> GetIOResultsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20)
        {
            throw new NotImplementedException();
        }
    }
}