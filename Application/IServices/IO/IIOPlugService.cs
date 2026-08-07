using Application.DTOs.RequestDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOPlugService
    {
        public Task<List<PlugItemDTO>> UpdatePort(PortRequest portRequest);
    }
}
