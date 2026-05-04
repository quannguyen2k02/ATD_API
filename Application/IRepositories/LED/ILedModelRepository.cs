
using Application.Common;
using Domain.Entities.LED;

namespace Application.IRepository.LED
{
    public interface ILedModelRepository
    {
        // Keyset pagination: lastId is the Id of the last record returned in the previous page.
        public Task<CursorPagedResult<LedModel>> GetLedModelAsync(int deviceId, string model, string kb, string fp, int? lastId = null, int pageSize = 20);
        public Task<LedModel> AddLedModelAsync(LedModel ledModel);
        public Task<LedModelConfig> AddLedModelConfig(LedModelConfig ledModelConfig);
        public Task<CursorPagedResult<LedModel>> GetLedModelsByDeviceIdAsync(int id, int? lastId = null, int pageSize = 20);
        public Task<LedModel> GetLedModelById(int id);

    }
}