using Domain.Enitties.IO;

namespace Application.DTOs.ResponseDTOs.IO
{
    public class IOConfigResponse
    {
        public int Id { get; set; }
        public int? IOModelId { get; set; }
        public ICollection<IOConfigDTO>? IOConfigs { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
