using Application.DTOs.RequestDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOResultService
    {
        Task<(int inserted, int skipped)> AddBatchAsync(IEnumerable<IOResultRequest> batch);
    }
}
