using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.IO
{
    public class IOTestDistanceRepository : GenericRepository<TestDistance>, IIOTestDistanceRepository
    {
        public IOTestDistanceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeleteTestDistanceByIOId(int id)
        {
            var items = await _context.TestDistances.Where(x => x.IOId == id).ToListAsync();
            if (items != null && items.Count > 0)
            {
                _context.TestDistances.RemoveRange(items);
                await _context.SaveChangesAsync();
            }
        }
    }
}
