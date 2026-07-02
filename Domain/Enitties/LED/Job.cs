using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.LED
{
    [Table("Job")]
    public class Job
    {
        public int Id { get; set; }
        public int LedStatusId { get; set; }
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
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual LedStatus? LedStatus { get; set; }
    }
}
