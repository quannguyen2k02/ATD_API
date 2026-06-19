using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("IOModel")]
    public class IOModel
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public string ModelName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
