using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.RequestDTOs.LED
{
    public class LedModelDTO

    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Device name is required.")]

        public string DeviceName {  get; set; }
        public string LineName { get; set; }
        [Ignore]
        public int LedId { get; set; }
        [Required(ErrorMessage = "Model name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "KB setting is required.")]

        public string KB { get; set; }
        [Required(ErrorMessage = "FP setting is required.")]

        public string FP { get; set; }
        public ICollection<LedModelConfigDTO> LEDModelConfigs { get; set; }
        public ICollection<LedCameraDTO>? Cameras { get; set; }
        [JsonIgnore]

        public DateTime CreateDate { get; set; }
        [JsonIgnore]

        public DateTime ModifiedDate { get; set; }
    }
}
