namespace Application.DTOs.ResponseDTOs.IO
{
    public class IOModelResponse
    {
        public int Id { get; set; }
        public int? IOId { get; set; }
        public string? ModelName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}