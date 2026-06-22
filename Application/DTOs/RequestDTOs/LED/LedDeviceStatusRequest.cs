namespace Application.DTOs.RequestDTOs.LED
{
    public class LedDeviceStatusRequest
    {
        public int LedDeviceId { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
