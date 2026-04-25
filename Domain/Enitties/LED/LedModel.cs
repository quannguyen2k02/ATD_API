using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.LED
{
    [Table("LedModel")]
    public class LedModel
    {
        public int Id { get; set; }
        public int LedId { get; set; }
        public string? Name { get; set; }
        public string? KB { get; set; }
        public string? FP { get; set; }
        public ICollection<LedModelConfig> LEDModelConfigs { get; set; }
        public ICollection<LedCamera>? Cameras { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
