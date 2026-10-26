using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("ReplacementRecord")]
    public class ReplacementRecord
    {
        public int Id { get; set; }
        public string? IOName { get; set; }
        public int? IOId { get; set; }
        public string? Port { get; set; }
        public string? MaxSpec { get; set; }
        public string? Spec { get; set; }
        public DateTime ReplaceDate { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeCode { get; set; }
    }
}
