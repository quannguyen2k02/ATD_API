namespace Application.DTOs.ResponseDTOs.IO
{
    public class IOResponse
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public string? AssetId { get; set; }
        public string? DeviceName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
