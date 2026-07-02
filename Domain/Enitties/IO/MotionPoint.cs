using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("MotionPoint")]
    public class MotionPoint
    {
        public int Id { get; set; }
        public int MotionPointsManagementId { get; set; }
        public double LeftX { get; set; }
        public double LeftY { get; set; }
        public double LeftZ { get; set; }
        public double RightX { get; set; }
        public double RightY { get; set; }
        public double RightZ { get; set; }
        public double BackX { get; set; }
        public double BackY { get; set; }
        public double BackZ { get; set; }
        public double HoldX { get; set; }
        public double HoldY { get; set; }
        public double HoldZ { get; set; }
        public double TransY { get; set; }
        public double MaxVel { get; set; }
    }
}
