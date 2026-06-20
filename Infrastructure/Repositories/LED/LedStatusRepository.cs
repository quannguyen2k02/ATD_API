using Application.IRepositories.LED;
using AutoMapper;
using Domain.Enitties.LED;
using Domain.Entities.LED;
using Infrastructure.Data;

namespace Infrastructure.Repositories.LED
{
    public class LedStatusRepository : GenericRepository<LedDeviceStatus>, ILedStatusRepository
    {

        public LedStatusRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}
