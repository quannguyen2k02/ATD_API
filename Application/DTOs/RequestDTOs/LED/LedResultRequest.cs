using AutoMapper.Configuration.Annotations;

namespace Application.DTOs.RequestDTOs.LED
{
    public class LedResultRequest
    {
        public int LEDId { get; set; }
        public string? SN { get; set; }
        public string? ModelName { get; set; }
        public string? Result { get; set; }
        public string? CT { get; set; }
        public string? FailItems { get; set; } = "";
        public DateTime DateTime = DateTime.Now;

    }
}
