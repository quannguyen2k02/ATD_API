using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.ChangeLog
{
    [Table("ChangedLog")]
    public class ChangedLog
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Log { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
