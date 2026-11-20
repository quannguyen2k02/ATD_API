using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("VccdItem")]
    public class VccdItem
    {
        public int Id { get; set; }
        public int IOId { get; set; }
        public string? Module { get; set; }
        public string? Camera { get; set; }
        public int ExposureTime { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
