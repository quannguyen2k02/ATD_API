namespace Application.DTOs.RequestDTOs.IO
{
    public class IOOffsetDTO
    {
        public string? Module { get; set; }
        public string? Port { get; set; }
        public double? X_Axis_Insertion { get; set; }
        public double? Y_Axis_Insertion { get; set; }
        public double? Z_Axis_Insertion { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
