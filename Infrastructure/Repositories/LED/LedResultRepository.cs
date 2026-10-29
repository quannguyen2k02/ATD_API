using Application.IRepositories.LED;
using Domain.Enitties.LED;
using Infrastructure.Data;

namespace Infrastructure.Repositories.LED
{
    public class LedResultRepository : GenericRepository<LedResult>, ILedResultRepository
    {
        public LedResultRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
