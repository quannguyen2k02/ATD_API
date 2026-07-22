

using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Enitties
{
    [Table("Line")]
    public class Line
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Domain.Entities.LED.LED>? Leds { get; set; }
        public ICollection<Domain.Enitties.LCD.LCD>? LCDs { get; set; }
        public ICollection<Domain.Enitties.IO.IO>? IOs { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
