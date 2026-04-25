using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Application.DTOs.RequestDTOs.LED
{
    public class JobDTO
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Left { get; set; }
        public string? Top { get; set; }
        public string? Width { get; set; }
        public string? Height { get; set; }
        public string? AreaMin_RGB { get; set; }
        public string? AreaMax_RGB { get; set; }
        public string? HolesAreaMin_RGB { get; set; }
        public string? HolesAreaMax_RGB { get; set; }
        public string? BlobNum_KBBL { get; set; }
        public string? AreaMin_Blob { get; set; }
        public string? AreaMax_Blob { get; set; }
        public string? RMin { get; set; }
        public string? RMax { get; set; }
        public string? GMin { get; set; }
        public string? GMax { get; set; }
        public string? BMin { get; set; }
        public string? BMax { get; set; }
        public string? IsKBLightTest { get; set; }
        public string? ThresholdMin { get; set; }
        public string? ThresholdMax { get; set; }
        public string? ThresholdMin_KBBL { get; set; }
        public string? ThresholdMax_KBBL { get; set; }
        public string? BlobNum { get; set; }
        public string? IsFlicker { get; set; }
        public string? EnableRGBScaleDif { get; set; }
        public string? RGBScaleDif { get; set; }
        public string? EnableMeanScale { get; set; }
        [JsonIgnore]

        public DateTime CreateDate { get; set; }
        [JsonIgnore]

        public DateTime ModifiedDate { get; set; }
    }
}
