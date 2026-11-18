using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.ResponseDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOStatusService
    {
        public Task<IOStatusResponse> AddIOStatusAsync(IOStatusRequest request);
    }
}
