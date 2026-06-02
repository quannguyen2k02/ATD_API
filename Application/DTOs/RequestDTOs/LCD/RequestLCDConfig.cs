using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RequestDTOs.LCD
{
    public class RequestLCDConfig
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
