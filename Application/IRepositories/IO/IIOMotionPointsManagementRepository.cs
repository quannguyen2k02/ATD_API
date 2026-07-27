using Application.Common;
using Domain.Enitties.IO;
using Domain.Entities.LED;
namespace Application.IRepositories.IO
{
    public interface IIOMotionPointsManagementRepository : IGenericRepository<MotionPointsManagement>
    {
        public Task<CursorPagedResult<MotionPointsManagement>> GetIOMotionPoints(int modelId, int? lastId = null, int pageSize = 20);

    }
}
