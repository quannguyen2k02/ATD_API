using Application.Common;
using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOMotionPointManagementService
    {
        public Task<IOMotionPointsResponse> AddNewIOMotionPoints(IOMotionPointsRequest motionPointsRequest);
        public Task<CursorPagedResult<dynamic>> GetMotionPoints(int modelId, int? lastId = null, int pageSize = 20);
    }
}
