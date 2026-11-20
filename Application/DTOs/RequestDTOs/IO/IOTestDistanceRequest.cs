namespace Application.DTOs.RequestDTOs.IO
{
    public class IOTestDistanceRequest
    {
        public int IOId { get; set; }
        public List<IOTestDistanceDTO> TestDistances { get; set; } = new();
    }
}
