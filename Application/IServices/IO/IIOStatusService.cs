using Application.DTOs.RequestDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOStatusService
    {
        public Task<IOStatusRequest> AddIOStatusAsync(IOStatusRequest request);
    }
}
