using Application.IRepositories.LCD;
using Infrastructure.Data;

namespace Infrastructure.Repositories.LCD
{
    public class LCDRepository : ILCDRepository
    {
        private readonly ApplicationDbContext _context;
        public LCDRepository(ApplicationDbContext context)
        {
            _context = context;
        } 
        public async Task<Domain.Enitties.LCD.LCD> GetLCDById(int id)
        {
            var lcd = await _context.LCDs.FindAsync(id);
            return lcd;
        }
    }
}
