using Application.Common;
using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOConfigManagementService
    {
        public Task<IOConfigResponse> AddNewConfig(IOConfigRequest config);
        public Task<CursorPagedResult<dynamic>> GetIOConfig(int modelId, int? lastId = null, int pageSize = 20);

    }
}
