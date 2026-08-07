namespace Application.DTOs.RequestDTOs.IO
{
    public class PlugItemDTO
    {
        public int Id { get; set; }
        public int? IOId { get; set; }
        public string? Module { get; set; }
        public string? Port { get; set; }
        public int? Limited { get; set; }
        public int? Actual { get; set; }
        public int? Warning { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
