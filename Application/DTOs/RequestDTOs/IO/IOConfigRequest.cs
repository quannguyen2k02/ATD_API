using Application.DTOs.ResponseDTOs.IO;

namespace Application.DTOs.RequestDTOs.IO
{
    public class IOConfigRequest
    {
        public int IOId { get; set; }
        public string ModelName { get; set; }
        public ICollection<IOConfigDTO>? IOConfigs { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
