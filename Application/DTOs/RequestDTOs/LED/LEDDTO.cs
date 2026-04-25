using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.RequestDTOs.LED
{
    public class LEDDTO
    {
        public string? Name { get; set; }
        public string? LedDeviceId { get; set; }
        public ICollection<LedModelDTO> LedModels { get; set; }
        [JsonIgnore]

        public DateTime CreateDate { get; set; }
        [JsonIgnore]

        public DateTime ModifiedDate { get; set; }
    }
}
