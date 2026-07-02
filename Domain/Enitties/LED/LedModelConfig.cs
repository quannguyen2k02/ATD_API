using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.LED
{
    [Table("LedModelConfig")]
    public class LedModelConfig
    {
        public int Id { get; set; }
        public int LedModelId { get; set; }
        public string? Delay_Status1 { get; set; }
        public string? Delay_Status2 { get; set; }
        public string? Delay_Status3 { get; set; }
        public string? Delay_Status4 { get; set; }
        public string? Delay_Status5 { get; set; }
        public string? Delay_Audio { get; set; }
        public string? Delay_FPTime { get; set; }
        public string? Delay_IRTest { get; set; }
        public string? Delay_FPTest { get; set; }
        public string? Delay_StatusButtonTime { get; set; }
        public string? Delay_GetSPKResult { get; set; }
        public string? Delay_WriteSNToAudio { get; set; }

        // --- General Enable Flags ---
        public string? EnableSaveSourcePic { get; set; }
        public string? PressedKB { get; set; }
        public string? EnableAudioTest { get; set; }
        public string? SKPTestPositionEnable { get; set; }

        // --- LED Test Enable Flags ---
        public string? EnableLedTest1 { get; set; }
        public string? EnableLedTest2 { get; set; }
        public string? EnableLedTest3 { get; set; }
        public string? EnableLedTest4 { get; set; }
        public string? EnableLedTest5 { get; set; }

        // --- Fingerprint (FP) Test Enable Flags ---
        public string? EnableFPTestStatus1 { get; set; }
        public string? EnableFPTestStatus2 { get; set; }
        public string? EnableFPTestStatus3 { get; set; }
        public string? EnableFPTestStatus4 { get; set; }
        public string? EnableFPTestStatus5 { get; set; }

        // --- Positional Data ---
        public string? FPPositionX { get; set; }
        public string? FPPositionY { get; set; }
        public string? FPPositionZ { get; set; }
        public string? KBSafeZ { get; set; }

        public string? Status12PosX { get; set; }
        public string? Status12PosY { get; set; }
        public string? Status12PosZ { get; set; }

        public string? Status23PosX { get; set; }
        public string? Status23PosY { get; set; }
        public string? Status23PosZ { get; set; }

        public string? Status34PosX { get; set; }
        public string? Status34PosY { get; set; }
        public string? Status34PosZ { get; set; }

        public string? Status45PosX { get; set; }
        public string? Status45PosY { get; set; }
        public string? Status45PosZ { get; set; }

        public string? EndPosX { get; set; }
        public string? EndPosY { get; set; }
        public string? EndPosZ { get; set; }

        public string? SPKTestPositionX { get; set; }
        public string? SPKTestPositionY { get; set; }
        public string? SPKTestPositionZ { get; set; }

        // --- Speaker (SPK) Test Status ---
        public string? SPKTestStatus1 { get; set; }
        public string? SPKTestStatus2 { get; set; }
        public string? SPKTestStatus3 { get; set; }
        public string? SPKTestStatus4 { get; set; }
        public string? SPKTestStatus5 { get; set; }

        // --- Read Speaker (SPK) Result Status ---
        public string? ReadSPKResultStatus1 { get; set; }
        public string? ReadSPKResultStatus2 { get; set; }
        public string? ReadSPKResultStatus3 { get; set; }
        public string? ReadSPKResultStatus4 { get; set; }
        public string? ReadSPKResultStatus5 { get; set; }
        public virtual LedModel? LedModel { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
