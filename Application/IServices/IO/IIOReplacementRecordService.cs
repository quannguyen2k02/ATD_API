using Application.DTOs.RequestDTOs.IO;

namespace Application.IServices.IO
{
    public interface IIOReplacementRecordService
    {
        Task<ImportResult> ImportAsync(
        List<ReplacementRecordRequest> rows, CancellationToken ct);
    }
}
