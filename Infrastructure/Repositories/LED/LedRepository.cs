using Application.IRepository.LED;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories.LED
{
    public class LedRepository : GenericRepository<Domain.Entities.LED.LED>, ILedRepository
    {
        public LedRepository(ApplicationDbContext context):base(context)
        {
        }

        public async Task<int?> GetDeviceIdByDeviceNameAndLineNameAsync(string deviceName, int lineId)
        {
            return await _context.LEDs.Where(x => x.Name == deviceName && x.LineId == lineId).Select(x=> (int?)x.Id).FirstOrDefaultAsync();
        }

        public async Task<string?> GetDeviceNameByIdAsync(int id)
        {
            return await _context.LEDs.Where(x=>x.Id == id).Select(x=>x.Name).FirstOrDefaultAsync();

        }
    }
}
