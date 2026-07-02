namespace Application.DTOs.RequestDTOs.LED.ChangeLog
{
    public class LedLogRequest
    {
        public int LedId { get; set; }
        public string? ModelName { get; set; }
        public int? KB { get; set; }
        public int? FP { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
} 
