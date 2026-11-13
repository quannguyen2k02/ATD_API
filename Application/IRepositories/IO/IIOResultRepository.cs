using Application.Common;
using Domain.Enitties.IO;
using Domain.Enitties.LCD;

namespace Application.IRepositories.IO
{
    public interface IIOResultRepository : IGenericRepository<IOResult>
    {
        public Task<IEnumerable<string>> GetExistingSNsAsync(IEnumerable<string> sns);
        public Task AddRangeAsync(IEnumerable<IOResult> results);
        public Task<CursorPagedResult<IOResult>> GetIOResultsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
    }
}
