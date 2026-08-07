using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Domain.Enitties.IO
{
    [Table("PlugItem")] 
    public class PlugItem
    {
        public int Id { get; set; }
        public int? IOId  { get; set; }
        public string? Module { get; set; }
        public string? Port { get; set; }
        public int? Limited { get; set; }
        public int? Actual { get; set; }
        public int? Warning { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
