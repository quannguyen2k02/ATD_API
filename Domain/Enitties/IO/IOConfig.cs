using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("IOConfig")]
    public class IOConfig
    {
        public int Id { get; set; }
        public int? IOConfigManagementId { get; set; }
        public string? Station { get; set; }
        public string? Cylinder { get; set; }
        public string? Port  { get; set; }
        public int? Retest { get; set; }
        public string? Template { get; set; }
        public int? LightSource1 { get; set; }
        public int? LightSource2 { get; set; }
        public int? Priority { get; set; }
        public int? PortNo { get; set; }
        public string? TestPosition { get; set; }
    }
}
