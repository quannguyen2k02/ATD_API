using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.LED
{
    [Table("LedDeviceStatus")]
    public class LedDeviceStatus
    {
        public int Id { get; set; }
        public int LedDeviceId { get; set; }
        public string? Status { get; set; }
        public string? Description   { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
