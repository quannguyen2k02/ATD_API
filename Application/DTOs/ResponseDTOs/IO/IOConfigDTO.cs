namespace Application.DTOs.ResponseDTOs.IO
{
    public class IOConfigDTO
    {
        public string? Station { get; set; }
        public string? Cylinder { get; set; }
        public string? Port { get; set; }
        public int? PortNo { get; set; }
        public int? Retest { get; set; }
        public string? Template { get; set; }
        public int? LightSource1 { get; set; }
        public int? LightSource2 { get; set; }
        public int? Priority { get; set; }
        public string? TestPosition { get; set; }
    }
}
