using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ResponseDTOs.LED
{
    public class LedDeviceStatusResponse
    {
        public int Id { get; set; }
        public int LedDeviceId { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
