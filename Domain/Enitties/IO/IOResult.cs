using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Enitties.IO
{
    [Table("IOResult")]
    public class IOResult
    {
        public int Id { get; set; }
        public int IOId { get; set; }
        public string? Model { get; set; }
        public string? SN { get; set; }
        public string? Result { get; set; }
        public int Retest { get; set; }
        public double CT { get; set; }
        public string? Left_TypeC_1 { get; set; }
        public string? Left_TypeC_2 { get; set; }
        public string? Left_TypeC_3 { get; set; }
        public string? Left_TypeC_4 { get; set; }
        public string? Left_HDMI { get; set; }
        public string? Left_USB_1 { get; set; }
        public string? Left_USB_2 { get; set; }
        public string? Left_USB_3 { get; set; }
        public string? Left_USB_4 { get; set; }
        public string? Left_SD { get; set; }
        public string? Left_Audio { get; set; }
        public string? Left_AudioV2 { get; set; }
        public string? Left_Docking { get; set; }
        public string? Left_Docking_1 { get; set; }
        public string? Left_Docking_2 { get; set; }
        public string? Left_Docking_3 { get; set; }
        public string? Left_Docking_4 { get; set; }
        public string? Right_TypeC_1 { get; set; }
        public string? Right_TypeC_2 { get; set; }
        public string? Right_TypeC_3 { get; set; }
        public string? Right_TypeC_4 { get; set; }
        public string? Right_HDMI { get; set; }
        public string? Right_USB_1 { get; set; }
        public string? Right_USB_2 { get; set; }
        public string? Right_USB_3 { get; set; }
        public string? Right_USB_4 { get; set; }
        public string? Right_SD { get; set; }
        public string? Right_Audio { get; set; }
        public string? Right_AudioV2 { get; set; }
        public string? Right_Docking { get; set; }
        public string? Right_Docking_1 { get; set; }
        public string? Right_Docking_2 { get; set; }
        public string? Right_Docking_3 { get; set; }
        public string? Right_Docking_4 { get; set; }
        public string? Back_TypeC_1 { get; set; }
        public string? Back_TypeC_2 { get; set; }
        public string? Back_HDMI { get; set; }
        public string? Back_USB1 { get; set; }
        public string? Back_USB2 { get; set; }
        public string? Back_Audio { get; set; }
        public string? Back_AudioV2 { get; set; }
        public string? Back_SD { get; set; }
        public string? Back_MiniSD { get; set; }
        public string? Back_Docking { get; set; }
        public string? Back_Docking_1 { get; set; }
        public string? Back_Docking_2 { get; set; }
        public string? Back_Docking_3 { get; set; }
        public string? Back_Docking_4 { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual IO? IO { get; set; }
    }
}
