using Domain.Entities.LED;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.LED
{
    [Table("LedResult")]
    public class LedResult
    {
        public int Id { get; set; }
        public int LEDId { get; set; }
        public string? SN { get; set; }
        public string? ModelName { get; set; }
        public string? Result { get; set; }
        public string? CT { get; set; }
        public string? FailItems { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Domain.Entities.LED.LED? LED { get; set; }
    }
}
