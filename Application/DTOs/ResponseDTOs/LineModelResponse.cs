namespace Application.DTOs.ResponseDTOs
{
    public class LineModelResponse
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
