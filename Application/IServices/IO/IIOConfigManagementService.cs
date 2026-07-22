using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOConfigManagementService
    {
        public Task<IOConfigResponse> AddNewConfig(IOConfigRequest config);
        
    }
}
