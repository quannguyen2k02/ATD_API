using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.LCD
{
    [Table("LCDConfig")]
    public class LCDConfig
    {
        public int Id { get; set; }
        public int LCDId { get; set; }
        public string? Camera { get; set; }
        public string? Light_Source { get; set; }
        public string? Scan { get; set; }
        public string? SoftPen_Pressure { get; set; }
        public string? HardPen_Pressure { get; set; }
        public string? Test_Position_Pressure { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
