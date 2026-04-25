using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.LED
{
    [Table("LedCamera")]
    public  class LedCamera
    {
        public int Id { get; set; }
        public int? LedModelId { get; set; }
        public string? Name { get; set; }
        public ICollection<LedStatus> LedStatuses { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
