using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RequestDTOs.LCD
{
    public class RequestLCDModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "LCDId is required.")]
        public int LCDId { get; set; }
        [Required(ErrorMessage = "ModelName is required.")]
        public string ModelName { get; set; }
        public string? Section_X_Start_Point { get; set; }
        public string? Section_Module_Start_Point { get; set; }
        public string? Section_Module_Angle_90_Point { get; set; }
        public string? Section_Module_Test_Point { get; set; }
        public string? Section_Module_Finish_Point { get; set; }
        public string? Locating_Module_Start_Point { get; set; }
        public string? Location_Module_Claim_Point { get; set; }
        public string? Location_Capture_Point { get; set; }
        public string? Test_Module_Start_Point { get; set; }
        public string? Space_Key_Middle_Test_Point { get; set; }
        public string? Space_Key_Middle_Test_Up_Point { get; set; }
        public string? SoftPen_Right_Up_Test_Point { get; set; }
        public string? SoftPen_Right_Down_Test_Point { get; set; }
        public string? SoftPen_Left_Down_Test_Point { get; set; }
        public string? SoftPen_Left_Up_Test_Point { get; set; }
        public string? HardPen_Right_Up_Test_Point { get; set; }
        public string? HardPen_Right_Down_Test_Point { get; set; }
        public string? HardPen_Left_Down_Test_Point { get; set; }
        public string? HardPen_Left_Up_Test_Point { get; set; }
        public string? BlackEdge_Left_Up_Capture_Point { get; set; }
        public string? BlackEdge_Left_Down_Capture_Point { get; set; }
        public string? BlackEdge_Right_Up_Capture_Point { get; set; }
        public string? BlackEdge_Right_Down_Capture_Point { get; set; }

        public string? BlackEdge_Middle_Down_Capture_Point { get; set; }

        public string? Top_Edge_Limit { get; set; }
        public string? Down_Edge_Limit { get; set; }
        public string? Left_Edge_Limit { get; set; }
        public string? Right_Edge_Limit { get; set; }
        public string? Offset_Top_Edge { get; set; }
        public string? Offset_Left_Edge { get; set; }
        public string? Offset_Down_Edge { get; set; }
        public string? Offset_Up_Edge { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
