using Application.DTOs.ResponseDTOs.IO;
using System.Dynamic;

namespace Infrastructure.ExternalServices.Mapper
{
    public static class IO_Mapper
    {
        public static dynamic Map_MotionPoint(IOMotionPointsResponse motionPoint)
        {
            dynamic result = new ExpandoObject();
            result.Id = motionPoint.Id;
            result.IOId = motionPoint.IOModelId;
            result.CreateDate = motionPoint.CreateDate;
            foreach (var motion in motionPoint.MotionPoints)
            {
                string key = $"{motion.MotionPointsName}";
                string value = "";
                if (motion.MotionPointsName.Contains("Left", StringComparison.OrdinalIgnoreCase)) 
                {
                    value = $"{motion.LeftX}_{motion.LeftY}_{motion.LeftZ}";
                }
                else if (motion.MotionPointsName.Contains("Right", StringComparison.OrdinalIgnoreCase)) {
                    value = $"{motion.RightX}_{motion.RightY}_{motion.RightZ}";
                }
                else if (motion.MotionPointsName.Contains("Back", StringComparison.OrdinalIgnoreCase)) {
                    value = $"{motion.BackX}_{motion.BackY}_{motion.BackZ}";
                }
                else if (motion.MotionPointsName.Contains("Hold", StringComparison.OrdinalIgnoreCase)) {
                    value = $"{motion.HoldX}_{motion.HoldY}_{motion.HoldZ}";
                }
                else if (motion.MotionPointsName.Contains("移栽Y", StringComparison.OrdinalIgnoreCase))
                {
                    value = motion.TransY;
                }
                else if(motion.MotionPointsName.Contains("Robot put", StringComparison.OrdinalIgnoreCase))
                {
                    value = $"{motion.HoldX}_{motion.HoldY}_{motion.HoldZ}";
                }
                else if(motion.MotionPointsName.Contains("左模组", StringComparison.OrdinalIgnoreCase))
                {
                    value = $"{motion.LeftX}_{motion.LeftY}_{motion.LeftZ}";
                }
                else if (motion.MotionPointsName.Contains("右模组", StringComparison.OrdinalIgnoreCase))
                {
                    value = $"{motion.RightX}_{motion.RightY}_{motion.RightZ}";
                }
                else if (motion.MotionPointsName.Contains("后模组", StringComparison.OrdinalIgnoreCase))
                {
                    value = $"{motion.BackX}_{motion.BackY}_{motion.BackZ}";
                }
                    ((IDictionary<string, object>)result)[key] = value;
            }
            return result;

        }
        public static dynamic Map_Offset(IOOffsetsResponse offsets)
        {
            dynamic result = new ExpandoObject();
            result.Id = offsets.Id;
            result.IOId = offsets.IOModelId;
            result.CreateDate = offsets.CreateDate;
            foreach(var offset in offsets.Offsets)
            {
                string key = $"{offset.Module}_{offset.Port}";
                string value = $"{offset.X_Axis_Insertion}_{offset.Y_Axis_Insertion}_{offset.Z_Axis_Insertion}";
                ((IDictionary<string, object>)result)[key] = value;
            }
            return result;
        }
        public static dynamic Map_Test_Config(IOConfigResponse configs)
        {
            dynamic result = new ExpandoObject();
            result.Id = configs.Id;
            result.IOId = configs.IOModelId;
            result.CreateDate = configs.CreateDate;
            foreach (var item in configs.IOConfigs)
            {
                string key = $"{item.Station}_{item.Port}";
                string value = $"{item.Cylinder}, Priority:{item.Priority}, Template: {item.Template}, Retest: {item.Retest}, LightSource1: {item.LightSource1}, LightSource2: {item.LightSource2}, PortNo:{item.PortNo}, TestPosition: {item.TestPosition}";
                ((IDictionary<string, object>)result)[key] = value;
            }
            return result;
        }
    }
}