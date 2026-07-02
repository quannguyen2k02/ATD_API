using Application.IRepositories.ChangeLog;
using Domain.Enitties.ChangeLog;
using Infrastructure.Data;

namespace Infrastructure.Repositories.ChangeLog
{
    public class LedLogRepository : GenericRepository<LEDLog>, ILedLogRepository
    {
        public LedLogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
