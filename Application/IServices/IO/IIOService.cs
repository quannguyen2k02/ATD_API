using Application.DTOs.ResponseDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOService
    {
        public Task<List<IOResponse>> GetIOsByLineId(int lineId);
    }
}
