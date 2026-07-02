using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("Offset")]
    public class Offset
    {
        public int Id { get; set; }
        public int OffsetManagementId { get; set; }
        public string? Module { get; set; }
        public string? Port { get; set; }
        public double? X_Axis_Insertion { get; set; }
        public double? Y_Axis_Insertion { get; set; }
        public double? Z_Axis_Insertion { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
