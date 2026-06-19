using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("IOOffset")]
    public class IOOffset
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
