namespace Application.DTOs.ResponseDTOs.LED
{
    public class LedResultResponse
    {
        public int LEDId { get; set; }
        public string? SN { get; set; }
        public string? ModelName { get; set; }
        public string? Result { get; set; }
        public string? CT { get; set; }
        public string? FailItems { get; set; }
        public string? LineName { get; set; }
        public string? DeviceName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
