using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;

namespace Infrastructure.Repositories.IO
{
    public class IOConfigManagementRepository : GenericRepository<IOConfigManagement>, IIOConfigManagementRepository
    {
        public IOConfigManagementRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
