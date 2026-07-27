using Application.Common;
using Domain.Enitties.IO;

namespace Application.IRepositories.IO
{
    public interface IIOConfigManagementRepository : IGenericRepository<IOConfigManagement>
    {
        public Task<CursorPagedResult<IOConfigManagement>> GetIOConfigManagement(int modelId, int? lastId = null, int pageSize = 20);

    }
}
