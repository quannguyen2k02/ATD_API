using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.IO
{
    public class IOPlugRepository : GenericRepository<PlugItem>, IIOPlugRepository
    {
        public IOPlugRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task DeletePortByIoId(int ioId)
        {
            var plugs = await _context.PlugItems.Where(x => x.IOId == ioId).ToListAsync();
            if (plugs != null && plugs.Count > 0)
            {
                _context.PlugItems.RemoveRange(plugs);
                await _context.SaveChangesAsync();
            }
        }
    }
}
