using Application.DTOs.RequestDTOs.LED;
using System.Dynamic;

namespace Infrastructure.ExternalServices.Mapper
{
    public static class LED_MapToDynamic
    {
        public static dynamic MapToDynamic(LedModelDTO model)
        {
            dynamic result = new ExpandoObject();
            result.Id = model.Id;
            result.ModelName = model.Name;
            result.KB = model.KB;
            result.TP = model.FP;
            result.CreateDate = model.CreateDate;

            var config = model.LEDModelConfigs?.FirstOrDefault();
            if (config != null)
            {
                result.status12PosXYZ = $"{config.Status12PosX}_{config.Status12PosY}_{config.Status12PosZ}";
                result.Status23PosXYZ = $"{config.Status23PosX}_{config.Status23PosY}_{config.Status23PosZ}";
                result.Status34PosXYZ = $"{config.Status34PosX}_{config.Status34PosY}_{config.Status34PosZ}";
                result.Status45PosXYZ = $"{config.Status45PosX}_{config.Status45PosY}_{config.Status45PosZ}";
                result.EndPosXYZ = $"{config.EndPosX}_{config.EndPosY}_{config.EndPosZ}";
                result.SpkTestStatus1 = config.SPKTestStatus1;
                result.SpkTestStatus2 = config.SPKTestStatus2;
                result.SpkTestStatus3 = config.SPKTestStatus3;
                result.SpkTestStatus4 = config.SPKTestStatus4;
                result.SpkTestStatus5 = config.SPKTestStatus5;
                result.readSPKResultStatus1 = config.ReadSPKResultStatus1;
                result.readSPKResultStatus2 = config.ReadSPKResultStatus2;
                result.readSPKResultStatus3 = config.ReadSPKResultStatus3;
                result.readSPKResultStatus4 = config.ReadSPKResultStatus4;
                result.readSPKResultStatus5 = config.ReadSPKResultStatus5;
                result.enableAudioTest = config.EnableAudioTest;
                result.enableLedTest1 = config.EnableLedTest1;
                result.enableLedTest2 = config.EnableLedTest2;
                result.enableLedTest3 = config.EnableLedTest3;
                result.enableLedTest4 = config.EnableLedTest4;
                result.enableLedTest5 = config.EnableLedTest5;
                result.kbSafeZ = config.KBSafeZ;
            }


            var allJobs = model.Cameras?
                .SelectMany(c => c.LedStatuses)
                .SelectMany(ls => ls.Jobs) ?? Enumerable.Empty<JobDTO>();

            foreach (var job in allJobs)
            {

                string roiKey = $"Roi_{job.Name}";
                string specKey = $"Spec_{job.Name}";
                string rgbKey = $"RGB_{job.Name}";

                string roiValue = $"{job.Left}_{job.Top}_{job.Width}_{job.Height}";
                string specValue = $"{job.AreaMin_RGB}_{job.AreaMax_RGB}"; // Có thể thêm holesArea nếu muốn
                string rgbValue = $"{job.RMin}_{job.GMin}_{job.BMin}"; // Lấy 3 giá trị min

                ((IDictionary<string, object>)result)[roiKey] = roiValue;
                ((IDictionary<string, object>)result)[specKey] = specValue;
                ((IDictionary<string, object>)result)[rgbKey] = rgbValue;
            }

            return result;
        }
    }
}
