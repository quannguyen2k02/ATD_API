namespace Application.DTOs.RequestDTOs.IO
{
    public class ReplacementRecordRequest
    {
        public string? IOName { get; set; }
        public int? IOId { get; set; }
        public string? Port { get; set; }
        public string? MaxSpec { get; set; }
        public string? Spec { get; set; }
        public DateTime ReplaceDate { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeCode { get; set; }
    }
    public class ImportResult
    {
        public int Total { get; set; }
        public int Inserted { get; set; }
        public int Skipped { get; set; }
    }
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(Exception inner)
            : base("Dữ liệu đã tồn tại", inner) { }
    }
}
