using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.RequestDTOs.LED
{
    public  class LedCameraDTO
    {
        public string? Name { get; set; }
        public ICollection<LedStatusDTO> LedStatuses { get; set; }
        [JsonIgnore]

        public DateTime CreateDate { get; set; }
        [JsonIgnore]

        public DateTime ModifiedDate { get; set; }
    }
}
