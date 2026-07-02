using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("IOConfigManagement")]
    public class IOConfigManagement
    {
        public int Id { get; set; }
        public int? IOModelId { get; set; }
        public ICollection<IOConfig>? IOConfigs { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
