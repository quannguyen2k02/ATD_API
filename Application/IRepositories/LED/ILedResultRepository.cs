using Application.Common;
using Domain.Enitties.LCD;
using Domain.Enitties.LED;

namespace Application.IRepositories.LED
{
    public interface ILedResultRepository : IGenericRepository<LedResult>
    {
        public Task AddBatchLedResult(IEnumerable<LedResult> ledResults);
        public Task<IEnumerable<string>> GetExistingSNsAsync(IEnumerable<string> sns);
        public Task<CursorPagedResult<LCDResult>> GetLedResultsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
    }
}
