using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("IOStatus")]
    public class IOStatus
    {
        public int Id { get; set; }
        public int IOId { get; set; }
        public string? Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
