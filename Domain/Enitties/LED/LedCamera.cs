using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.LED
{
    [Table("LedCamera")]
    public  class LedCamera
    {
        public int Id { get; set; }
        public int? LedModelId { get; set; }
        public string? Name { get; set; }
        public ICollection<LedStatus> LedStatuses { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
