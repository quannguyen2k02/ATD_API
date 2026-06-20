using Application.Common;
using Domain.Enitties.LCD;

namespace Application.IRepositories.LCD
{
    public interface ILCDResultRepository:IGenericRepository<LCDResult>
    {
        public Task<IEnumerable<string>> GetExistingSNsAsync(IEnumerable<string> sns);
        public Task AddRangeAsync(IEnumerable<LCDResult> results);
        public Task<CursorPagedResult<LCDResult>> GetLCDResultsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
    }
}
