using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs.LED;

namespace Application.IServices.LED
{
    public interface ILedResultService
    {
        public Task<LedResultResponse> AddnewLedResult(LedResultRequest ledResultRequest);

    }
}
