using Application.IRepositories.LCD;
using Domain.Enitties.LCD;
using Infrastructure.Data;

namespace Infrastructure.Repositories.LCD
{
    public class LCDResultDemoRepository : GenericRepository<LCDResult>, ILCDResultDemoRepository
    {
        public LCDResultDemoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
