using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ResponseDTOs.LED
{
    public class LedResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? AssetId { get; set; }
    }
}
