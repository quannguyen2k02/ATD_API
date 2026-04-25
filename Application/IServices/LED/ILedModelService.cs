using Application.Common;
using Application.DTOs.RequestDTOs.LED;

namespace Application.IServices.LED
{
    public interface ILedModelService
    {
        public Task<LedModelDTO> AddLedModelAsync(LedModelDTO ledModelDTO);
        // Keyset pagination: client supplies lastId (id of last item from previous page). If null, return first page.
        public Task<PagedResult<dynamic>> GetLedModelAsync(string line, string devicename, string model, string kb, string fp, int? lastId = null, int pageSize = 20);
        public Task<PagedResult<dynamic>> GetLedModelsByDevice(string line, string devicename, int? lastId = null, int pageSize = 20);
        public Task<dynamic> GetLedModelById(int id);
    }
}
