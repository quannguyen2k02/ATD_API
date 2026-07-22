using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("PressureManagement")]
    public class PressureManagement
    {
        public int Id { get; set; }
        public int IOModelId { get; set; }
        public ICollection<PressureItem>? PressureItems { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
