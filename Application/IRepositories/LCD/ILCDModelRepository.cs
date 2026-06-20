

using Application.Common;
using Domain.Enitties.LCD;

namespace Application.IRepositories.LCD
{
    public interface ILCDModelRepository:IGenericRepository<LCDModel>
    {
        public Task<CursorPagedResult<LCDModel>> GetLCDModelsByDeviceIdAsync(int deviceId, int? lastId = null, int pageSize = 20);
        public Task<CursorPagedResult<LCDModel>> GetLCDModelsByModelNameAsync(string modelName, int deviceId, int? lastId = null, int pageSize = 20);
    }
}
