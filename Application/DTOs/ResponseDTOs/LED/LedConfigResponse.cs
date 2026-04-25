using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ResponseDTOs.LED
{
    public class LedConfigResponse
    {

        public int ID { get; set; }
        public int LedId { get; set; }
        public string? Camera1 { get; set; }
        public string? Camera2 { get; set; }
        public string? Camera3 { get; set; }
        public string? Camera4 { get; set; }
        public string? Camera5 { get; set; }
        public string? Scanner { get; set; }
        public string? RegexSN { get; set; }
        public string? RegexAudio { get; set; }
        public string? Delay_NBArrived { get; set; }
        public string? Delay_NBLocation { get; set; }
        public string? Delay_LCDLocation { get; set; }
        public string? Delay_ClearLine { get; set; }
        public string? Timeout_Cylinder { get; set; }
        public string? Timeout_NBArrive { get; set; }
        public string? Timeout_Axis { get; set; }
        public string? Timeout_ScannerConnectSingle { get; set; }
        public string? Timeout_ScannerConnectTotal { get; set; }
        public string? Timeout_Scanner { get; set; }
        public string? Enable_RGBButton { get; set; }
        public string? Enable_MainBeltWaitManualBelt { get; set; }
        public string? CheckSN { get; set; }
        public string? ScanError { get; set; }
        public string? LeftToRight { get; set; }
        public string? Compress { get; set; }
        public string? CompressLevel { get; set; }
        public string? EnableNGHold { get; set; }
        public string? EnableFails { get; set; }
        public string? Fails { get; set; }
        public string? StageCode_SFCS { get; set; }
        public string? KeyInfoType_SFCS { get; set; }
        public string? UniqueCheck_SFCS { get; set; }
        public string? InfoName_SFCS { get; set; }
        public string? InfoValue { get; set; }
        public string? WebService_Path { get; set; }
        public string? LineName { get; set; }
        public string? SaftyPosition { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
