using Application.Common;
using Domain.Enitties.LCD;

namespace Application.IRepositories.LCD
{
    public interface ILCDConfigRepository : IGenericRepository<LCDConfig>
    {
        public Task<CursorPagedResult<LCDConfig>> GetLCDConfigsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
    }
}
