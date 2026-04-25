using Application.Common;
using Domain.Entities.LED;

namespace Application.IRepositories.LED
{
    public interface ILedConfigRepository
    {
        public Task<PagedResult<LedConfig>> GetLedConfigByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
        public Task<LedConfig> AddLedModelAsync(LedConfig ledConfig);
    }
}
