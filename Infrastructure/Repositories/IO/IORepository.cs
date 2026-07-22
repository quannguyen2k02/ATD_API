using Application.IRepositories.IO;
using Infrastructure.Data;

namespace Infrastructure.Repositories.IO
{
    public class IORepository : GenericRepository<Domain.Enitties.IO.IO>, IIORepository
    {
        public IORepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
