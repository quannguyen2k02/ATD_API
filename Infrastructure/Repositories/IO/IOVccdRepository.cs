using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.IO
{
    public class IOVccdRepository : GenericRepository<VccdItem>, IIOVccdRepository
    {
        public IOVccdRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task DeleteVccdByIOId(int id)
        {
            var vccds = await _context.VccdItems.Where(x => x.IOId == id).ToListAsync();
            if (vccds != null && vccds.Count > 0)
            {
                _context.VccdItems.RemoveRange(vccds);
                await _context.SaveChangesAsync();
            }
        }
    }
}
