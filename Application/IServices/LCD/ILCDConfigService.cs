using Application.Common;
using Application.DTOs.RequestDTOs.LCD;
using Application.DTOs.ResponseDTOs.LCD;
using Domain.Enitties.LCD;

namespace Application.IServices.LCD
{
    public interface ILCDConfigService
    {
        public Task<ResponseLCDConfig> AddLCDConfigAsync(RequestLCDConfig lcdConfig);
        public Task<ResponseLCDConfig> GetLCDConfigByIdAsync(int id);
        public Task<CursorPagedResult<ResponseLCDConfig>> GetLCDConfigsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
    }
}
