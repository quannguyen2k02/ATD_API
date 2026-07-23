using Domain.Enitties.IO;

namespace Application.DTOs.RequestDTOs.IO
{
    public class IOPressureRequest
    {
        public int IOId { get; set; }
        public string ModelName { get; set; }
        public ICollection<PressureItemDTO>? PressureItems { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
