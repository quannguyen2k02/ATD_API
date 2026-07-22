using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOOffsetManagementService
    {
        public Task<IOOffsetsResponse> AddNewOffset(IOOffsetsRequest offsetsRequest);
    }
}
