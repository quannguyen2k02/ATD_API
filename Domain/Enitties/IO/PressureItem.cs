using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("PressureItem")]
    public class PressureItem
    {
        public int Id { get; set; }
        public int PressureManagementId { get; set; }
        public string? Port { get; set; }
        public double? Pressure { get; set; }
        public DateTime CreateDate { get; set; }
    }
}