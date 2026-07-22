using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("IOModel")]
    public class IOModel
    {
        public int Id { get; set; }
        public int? IOId { get; set; }
        public string? ModelName { get; set; }
        public ICollection<OffsetManagement>? OffsetManagements { get; set; }
        public ICollection<IOConfigManagement>? ConfigManagements { get; set; }
        public ICollection<MotionPointsManagement>? MotionPointsManagements { get; set; }
        public ICollection<PressureManagement>? PressureManagements { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
