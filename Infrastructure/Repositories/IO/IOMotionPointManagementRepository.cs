using Application.IRepositories.IO;
using Domain.Enitties.IO;
using Infrastructure.Data;

namespace Infrastructure.Repositories.IO
{
    public class IOMotionPointManagementRepository : GenericRepository<MotionPointsManagement>, IIOMotionPointsManagementRepository
    {
        public IOMotionPointManagementRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}