

using Application.DTOs.ResponseDTOs.LED;

namespace Application.IServices.LED
{
    public interface ILedService
    {
        public Task<List<LedResponse>> GetLedsByLineId(int lineId);
    }
}
