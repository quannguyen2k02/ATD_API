using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ResponseDTOs.LCD
{
    public class LCDResponse
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public string? AssetId { get; set; }
        public string? DeviceName { get; set; }
    }
}
