using Application.IRepositories;
using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;

namespace Infrastructure.Repositories.IO
{
    public class IOOffsetManagementRepository : GenericRepository<OffsetManagement>, IIOOffsetManagementRepository
    {
        public IOOffsetManagementRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
