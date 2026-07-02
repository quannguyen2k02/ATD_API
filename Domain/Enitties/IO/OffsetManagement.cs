using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("OffsetManagement")]
    public class OffsetManagement
    {
        public int Id { get; set; }
        public int IOModelId { get; set; }
        public ICollection<Offset>? Offsets { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
