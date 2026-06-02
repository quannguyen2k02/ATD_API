using Application.Common;
using Domain.Enitties.LCD;

namespace Application.IRepositories.LCD
{
    public interface ILCDConfigRepository
    {
        public Task<LCDConfig> AddLCDConfigAsync(LCDConfig model);
        public Task<LCDConfig> GetLCDConfigByIdAsync(int id);
        public Task<CursorPagedResult<LCDConfig>> GetLCDConfigsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
    }
}
