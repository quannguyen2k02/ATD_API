using Application.DTOs.RequestDTOs.IO;

namespace Application.DTOs.ResponseDTOs.IO
{
    public class IOPressureResponse
    {
        public int Id { get; set; }
        public int IOModelId { get; set; }
        public ICollection<PressureItemDTO>? PressureItems { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
