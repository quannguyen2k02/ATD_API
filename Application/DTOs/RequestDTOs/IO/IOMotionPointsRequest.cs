using Application.DTOs.ResponseDTOs.IO;
namespace Application.DTOs.RequestDTOs.IO
{
    public class IOMotionPointsRequest
    {
        public int IOId { get; set; }
        public string ModelName { get; set; }
        public ICollection<IOMotionItemDTO>? MotionPoints { get; set; }
    }
}