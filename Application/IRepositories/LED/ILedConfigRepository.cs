using Application.Common;
using Domain.Entities.LED;

namespace Application.IRepositories.LED
{
    public interface ILedConfigRepository : IGenericRepository<LedConfig>
    {
        public Task<CursorPagedResult<LedConfig>> GetLedConfigByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
    }
}
