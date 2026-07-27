using Application.Common;
using Domain.Enitties.IO;
namespace Application.IRepositories.IO
{
    public interface IIOOffsetManagementRepository : IGenericRepository<OffsetManagement>
    {
        public Task<CursorPagedResult<OffsetManagement>> GetOffsetManagement(int modelId, int? lastId = null, int pageSize = 20);

    }
}