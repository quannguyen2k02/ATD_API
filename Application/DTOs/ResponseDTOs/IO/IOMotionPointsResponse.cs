namespace Application.DTOs.ResponseDTOs.IO
{
    public class IOMotionPointsResponse
    {
        public int Id { get; set; }
        public int IOModelId { get; set; }
        public ICollection<IOMotionItemDTO>? MotionPoints { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
