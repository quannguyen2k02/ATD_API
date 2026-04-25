using Application.DTOs.ResponseDTOs;

namespace Application.IServices.LED
{
    public interface ILineService
    {
        public Task<int?> GetIdByLineName(string name);
        public Task<string?> GetLineNameById(int id);
        public Task<List<LineModelResponse>> GetAllLinesAsync();
    }
}
