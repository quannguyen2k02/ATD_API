namespace Application.DTOs.RequestDTOs.LED
{
    public class LedResultRequest
    {
        public int LEDId { get; set; }
        public string? SN { get; set; }
        public string? ModelName { get; set; }
        public string? TypeModel { get; set; }
        public string? Result { get; set; }
        public int? CT { get; set; }
        public string? N_Camera { get; set; }
        public string? F4 { get; set; }
        public string? Close_F4 { get; set; }
        public string? Close_Caps { get; set; }
        public string? Caps { get; set; }
        public string? Num { get; set; }
        public string? Close_Num { get; set; }
        public string? Charge_White { get; set; }
        public string? Charge_Yellow { get; set; }
        public string? KBLight { get; set; }
        public string? KB_Close { get; set; }
        public string? Power { get; set; }
        public DateTime DateTime { get; set; }

    }
}
