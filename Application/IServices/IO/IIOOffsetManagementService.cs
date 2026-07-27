using Application.Common;
using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOOffsetManagementService
    {
        public Task<IOOffsetsResponse> AddNewOffset(IOOffsetsRequest offsetsRequest);
        public Task<CursorPagedResult<dynamic>> GetOffsets(int modelId, int? lastId = null, int pageSize = 20);

    }
}
