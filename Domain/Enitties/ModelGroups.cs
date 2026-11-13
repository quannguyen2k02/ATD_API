using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties
{
    [Table("ModelGroup")]
    public class ModelGroup
    {
        public int Id { get; set; }
        public string? RawModelName { get; set; } 
        public string? GroupModelName { get; set; } 
    }
}
