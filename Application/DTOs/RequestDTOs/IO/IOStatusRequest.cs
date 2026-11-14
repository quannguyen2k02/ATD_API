namespace Application.DTOs.RequestDTOs.IO
{
    public class IOStatusRequest
    {
        public int IOId { get; set; }
        public string? Status { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
