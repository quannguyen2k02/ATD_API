namespace Application.DTOs.RequestDTOs.IO
{
    public class IOVccdRequest
    {
        public int IOId { get; set; }
        public List<IOVccdDTO> Vccds { set; get; }
    }
}
