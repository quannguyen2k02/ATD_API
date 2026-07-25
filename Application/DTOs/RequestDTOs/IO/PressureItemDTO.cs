namespace Application.DTOs.RequestDTOs.IO
{
    public class PressureItemDTO
    {
        public string Port { get; set; }
        public double Pressure { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
