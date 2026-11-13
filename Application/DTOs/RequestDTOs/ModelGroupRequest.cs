using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.RequestDTOs
{
    public class ModelGroupRequest
    {
        [Required]
        public string RawModelName { get; set; }
        [Required]
        public string GroupModelName { get; set; }
    }
}
