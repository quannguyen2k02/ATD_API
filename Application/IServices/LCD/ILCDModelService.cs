using Application.Common;
using Application.DTOs.RequestDTOs.LCD;
using Application.DTOs.ResponseDTOs.LCD;

namespace Application.IServices.LCD
{
    public interface ILCDModelService
    {
            public Task<ResponseLCDModel> AddNewLCDModelAsync(RequestLCDModel model);
            public Task<CursorPagedResult<ResponseLCDModel>> GetLCDModelsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
            public Task<CursorPagedResult<ResponseLCDModel>> GetLCDModelsByModelNameAsync(string modelName, int deviceId, int? lastId = null, int pageSize = 20);
    }
}
