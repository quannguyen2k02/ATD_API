using Domain.Enitties.LCD;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("IO")]
    public class IO
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public string? AssetId { get; set; }
        public string? DeviceName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<IOModel>? IOModels { get; set; }
        public ICollection<PlugItem>? PlugItems { get; set; }
        public ICollection<ReplacementRecord>? ReplacementRecords { get; set; }
        public ICollection<IOResult>? IOResults { get; set; }
        public ICollection<IOStatus>? IOStatuses { get; set; }
        public ICollection<VccdItem>? VccdItems { get; set; }
        public ICollection<TestDistance>? TestDistances { get; set; }
        public virtual Line? Line { get; set; }
    }
}
