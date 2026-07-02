namespace Domain.Enitties.ChangeLog
{
    public class LEDLog
    {
        public int Id { get; set; }
        public int LedId { get; set; }
        public string? ModelName { get; set; }
        public string? KB { get; set; }
        public string? FP { get; set; }
        public DateTime CreateDate { get; set; }
        public LEDLog( int ledId, string? modelName, string? kB, string? fP)
        {
            LedId = ledId;
            ModelName = modelName;
            KB = kB;
            FP = fP;
            CreateDate = DateTime.Now;
        }
    }
}
