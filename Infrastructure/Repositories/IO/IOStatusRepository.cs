using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;

namespace Infrastructure.Repositories.IO
{
    public class IOStatusRepository : GenericRepository<IOStatus>, IIOStatusRepository
    {
        public IOStatusRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
