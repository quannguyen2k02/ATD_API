namespace Application.DTOs.ResponseDTOs.Identity
{
    public class UserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
