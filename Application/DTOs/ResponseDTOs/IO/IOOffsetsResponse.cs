using Application.DTOs.RequestDTOs.IO;

namespace Application.DTOs.ResponseDTOs.IO
{
    public class IOOffsetsResponse
    {
        public int Id { get; set; }
        public int IOModelId { get; set; }
        public ICollection<IOOffsetDTO>? Offsets { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
