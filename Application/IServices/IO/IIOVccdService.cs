using Application.DTOs.RequestDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOVccdService
    {
        public Task<List<IOVccdDTO>> UpdateVccd(IOVccdRequest vccdRequest);
    }
}