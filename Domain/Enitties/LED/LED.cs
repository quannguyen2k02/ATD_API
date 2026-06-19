using Domain.Enitties.LED;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.LED
{
    [Table("LED")]
    public class LED
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? AssetId { get; set; }
        public int LineId { get; set; }
        public ICollection<LedModel>? LedModels { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<LedConfig>? LedConfigs { get; set; }
        public ICollection<LedResult>? LedResults { get; set; }
    }
}
