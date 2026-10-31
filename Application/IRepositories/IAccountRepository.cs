using Application.DTOs.RequestDTOs.Identity;
using Application.DTOs.ResponseDTOs.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IAccountRepository
    {
        Task<string> SignInAsync(SignInModel model);
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<IdentityResult> AssignRoleAsync(AssignRoleModel model);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
        Task<IdentityResult> RemoveRoleAsync(AssignRoleModel model, string currentUserCode);
        Task<IdentityResult> DeleteUserAsync(string employeeCode, string currentUserCode);
    }
}
