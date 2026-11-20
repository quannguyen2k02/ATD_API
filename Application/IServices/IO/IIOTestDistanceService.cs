using Application.DTOs.RequestDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOTestDistanceService
    {
        public Task<List<IOTestDistanceDTO>> UpdateTestDistance(IOTestDistanceRequest request);
    }
}
