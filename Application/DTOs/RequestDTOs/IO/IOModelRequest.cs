using Domain.Enitties.IO;

namespace Application.DTOs.RequestDTOs.IO
{
    public class IOModelRequest
    {
        public int? IOId { get; set; }
        public string? ModelName { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
