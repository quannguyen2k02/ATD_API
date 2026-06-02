using Application.IRepositories.LED;
using AutoMapper;
using Domain.Enitties.LED;
using Infrastructure.Data;

namespace Infrastructure.Repositories.LED
{
    public class LedStatusRepository : ILedStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public LedStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<LedDeviceStatus> AddNewStatus(LedDeviceStatus ledDeviceStatus)
        {
            ledDeviceStatus.CreateDate = DateTime.Now;
            await _context.LedDeviceStatuses.AddAsync(ledDeviceStatus);
            await _context.SaveChangesAsync();
            return ledDeviceStatus;
        }
    }
}
