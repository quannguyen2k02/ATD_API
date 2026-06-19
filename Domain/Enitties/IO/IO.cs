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
    }
}
