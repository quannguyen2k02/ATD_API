using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("MotionPointsManagement")]
    public class MotionPointsManagement
    {
        public int Id { get; set; }
        public int IOModelId { get; set; }
        public ICollection<MotionPoint>? MotionPoints { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
