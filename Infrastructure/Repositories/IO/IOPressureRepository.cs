using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;

namespace Infrastructure.Repositories.IO
{
    public class IOPressureRepository : GenericRepository<PressureManagement>, IIOPressureManagementRepository
    {
        public IOPressureRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
