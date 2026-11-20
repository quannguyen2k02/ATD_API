using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("TestDistance")]
    public class TestDistance
    {
        public int Id { get; set; }
        public int IOId { get; set; }
        public string? Module { get; set; }
        public int Distance { get; set; }
    }
}
