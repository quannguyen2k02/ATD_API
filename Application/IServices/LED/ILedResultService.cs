using Application.Common;
using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs.LED;

namespace Application.IServices.LED
{
    public interface ILedResultService
    {
        public Task<(int inserted, int skipped)> AddBatchLedResultAsync(IEnumerable<LedResultRequest> ledResults);
        public Task<IEnumerable<string>> GetExistingSNsAsync(IEnumerable<string> sns);
        public Task<CursorPagedResult<LedResultResponse>> GetLedResultsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
    }
}
