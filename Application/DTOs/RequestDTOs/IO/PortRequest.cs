namespace Application.DTOs.RequestDTOs.IO
{
    public class PortRequest
    {
        public int IOId { get; set; }
        public List<PlugItemDTO> PlugItems { get; set; }
    }
}
