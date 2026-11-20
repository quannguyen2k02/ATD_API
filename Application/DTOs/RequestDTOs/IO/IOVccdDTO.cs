namespace Application.DTOs.RequestDTOs.IO
{
    public class IOVccdDTO
    {
        public int? IOId { get; set; }
        public string? Module { get; set; }
        public string? Camera { get; set; }
        public int ExposureTime { get; set; }
        public DateTime CreatedDate = DateTime.Now;
    }
}
