using Domain.Entities;
using Domain.Entities.LED;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties
{
    [Table("Line")]
    public class Line
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<LED>? Leds { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
