using Application.IRepositories.LCD;
using Domain.Enitties.LCD;
using Infrastructure.Data;

namespace Infrastructure.Repositories.LCD
{
    public class LCDRepository : GenericRepository<Domain.Enitties.LCD.LCD>, ILCDRepository
    {
        public LCDRepository(ApplicationDbContext context):base(context)
        {

        } 

    }
}
