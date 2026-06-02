namespace Application.DTOs.ResponseDTOs.LCD
{
    public class ResponseLCDResult
    {
        public int Id { get; set; }
        public int LCDId { get; set; }
        public string? ModelName { get; set; }
        public string? SN { get; set; }
        public string? TestItem { get; set; }
        public double? CT { get; set; }
        public double? Pressure { get; set; }
        public string? Result { get; set; }
        public string? Space_Key_Middle_Test_Point { get; set; }
        public string? Space_Key_Middle_Test_Up { get; set; }
        public string? SoftPen_Right_Up_Test_Point { get; set; }
        public string? SoftPen_Right_Down_Test_Point { get; set; }
        public string? SoftPen_Left_Down_Test_Point { get; set; }
        public string? HardPen_Left_Down_Test_Point { get; set; }
        public string? HardPen_Right_Up_Test_Point { get; set; }
        public string? HardPen_Left_Up_Test_Point { get; set; }
        public string? BlackEdge_Left_Up_Capture_Point { get; set; }
        public string? BlackEdge_Left_Down_Capture_Point { get; set; }
        public string? BlackEdge_Right_Up_Capture_Point { get; set; }
        public string? BlackEdge_Right_Down_Capture_Point { get; set; }
        public string? BlackEdge_Middle_Down_Capture_Point { get; set; }
        public string? Top_Left { get; set; }
        public string? Top_Right { get; set; }
        public string? Top_Deviation { get; set; }
        public string? Bottom_Left { get; set; }
        public string? Bottom_Right { get; set; }
        public string? Bottom_Middle { get; set; }
        public string? Bottom_Deviation { get; set; }
        public string? Left_Top { get; set; }
        public string? Left_Bottom { get; set; }
        public string? Left_Deviation { get; set; }
        public string? Right_Top { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Right_Bottom { get; set; }
        public string? Right_Deviation { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
