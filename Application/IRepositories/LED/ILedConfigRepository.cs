using Application.Common;
using Domain.Entities.LED;

namespace Application.IRepositories.LED
{
    public interface ILedConfigRepository
    {
        public Task<CursorPagedResult<LedConfig>> GetLedConfigByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
        public Task<LedConfig> AddLedModelAsync(LedConfig ledConfig);
    }
}
