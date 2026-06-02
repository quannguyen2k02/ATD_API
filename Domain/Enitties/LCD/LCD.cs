using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.LCD
{

    [Table("LCD")]
    public class LCD
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public string? AssetId { get; set; }
        public string? DeviceName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<LCDModel>? LCDModels { get; set; }
        public ICollection<LCDConfig>? LCDConfigs { get; set; }
        public ICollection<LCDResult>? lCDResults { get; set; }

    }
}
