using Domain.Enitties.IO;

namespace Application.DTOs.RequestDTOs.IO
{
    public class IOOffsetsRequest
    {
        public int IOId { get; set; }
        public string ModelName { get; set; }
        public ICollection<IOOffsetDTO>? Offsets { get; set; }
        public DateTime CreateDate
        {
            get; set;
        }
    }
}