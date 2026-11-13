using Application.DTOs.RequestDTOs.Identity;
using Application.DTOs.ResponseDTOs.Identity;
using Application.IRepositories;
using Application.IServices;
using Domain.Contants;
using Domain.Enitties;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AccountRepository(UserManager<ApplicationUser> userManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task<IdentityResult> AssignRoleAsync(AssignRoleModel model)
        {
            // 1. Tìm User theo mã nhân viên
            var user = await _userManager.FindByNameAsync(model.EmployeeCode);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"Không tìm thấy nhân viên có mã {model.EmployeeCode}"
                });
            }

            // 2. Kiểm tra Role có tồn tại trong hệ thống không
            if (!await _roleManager.RoleExistsAsync(model.RoleName))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotFound",
                    Description = $"Quyền '{model.RoleName}' không tồn tại trong hệ thống"
                });
            }

            // 3. Kiểm tra xem User đã có Role này chưa
            if (await _userManager.IsInRoleAsync(user, model.RoleName))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleAlreadyAssigned",
                    Description = $"Nhân viên này đã có quyền '{model.RoleName}' rồi"
                });
            }

            // 4. Thêm Role cho User
            var result = await _userManager.AddToRoleAsync(user, model.RoleName);

            // 5. (Quan trọng) Cập nhật SecurityStamp để vô hiệu hóa các Token cũ của user này
            // Nếu không cập nhật, user đó vẫn dùng token cũ (không có quyền mới) cho đến khi hết hạn.
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }

            return result;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            // Lấy toàn bộ user từ DB (Lưu ý: Nếu dữ liệu lớn, nên phân trang)
            var users = _userManager.Users.ToList();
            var result = new List<UserDTO>();

            foreach (var user in users)
            {
                // Lấy danh sách quyền của user đó
                var roles = await _userManager.GetRolesAsync(user);

                result.Add(new UserDTO
                {
                    Id = user.Id,
                    EmployeeCode = user.UserName!, // Identity lưu ở UserName
                    Roles = roles.ToList()
                });
            }

            return result;
        }

        public async Task<IdentityResult> RemoveRoleAsync(AssignRoleModel model, string currentUserCode)
        {
            // 1. CHẶN TỰ XÓA QUYỀN ADMIN CỦA CHÍNH MÌNH
            if (model.EmployeeCode == currentUserCode && model.RoleName == Roles.Administrator)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "CannotRemoveOwnAdminRole",
                    Description = "Bạn không thể tự xóa quyền Administrator của chính mình để tránh khóa hệ thống!"
                });
            }

            // 2. Tìm User
            var user = await _userManager.FindByNameAsync(model.EmployeeCode);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"Không tìm thấy nhân viên {model.EmployeeCode}"
                });
            }

            // 3. Kiểm tra xem User có quyền này không
            if (!await _userManager.IsInRoleAsync(user, model.RoleName))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotAssigned",
                    Description = $"Nhân viên này không có quyền '{model.RoleName}' để xóa"
                });
            }

            // 4. Thực hiện xóa quyền
            var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);

            // 5. Cập nhật SecurityStamp để vô hiệu hóa các Token cũ (buộc user phải đăng nhập lại)
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }

            return result;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            // 1. Tìm user
            var user = await _userManager.FindByNameAsync(model.EmployeeCode);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"Không tìm thấy nhân viên {model.EmployeeCode}"
                });
            }

            // 2. Tạo token reset password (chuẩn của Identity)
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // 3. Thực hiện đổi mật khẩu
            // Hàm này sẽ tự động kiểm tra mật khẩu mới có đúng chuẩn (độ dài, chữ, số...) không
            var result = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

            // 4. (Tuỳ chọn) Cập nhật SecurityStamp để đăng xuất user này khỏi mọi thiết bị
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
            }

            return result;
        }

        public async Task<AuthResponse> SignInAsync(SignInModel model)
        {
            var user = await _userManager.FindByNameAsync(model.EmployeeCode);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return null!;
            var expiredTokens = await _context.RefreshTokens
                .Where(x => x.UserId == user.Id && (x.ExpiresAt < DateTime.UtcNow || x.IsRevoked))
                .ToListAsync();

                if (expiredTokens.Any())
                {
                    _context.RefreshTokens.RemoveRange(expiredTokens);
                }
            var accessToken = await _tokenService.GenerateTokenAsync(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Lưu Refresh Token vào DB
            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            });
            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(15)
            };
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser { UserName = model.EmployeeCode };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Mặc định user mới là IDL
                await _userManager.AddToRoleAsync(user, Roles.DL);
            }
            return result;
        }


        public async Task<IdentityResult> DeleteUserAsync(string employeeCode, string currentUserCode)
        {
            // 1. CHẶN TỰ XÓA TÀI KHOẢN CỦA CHÍNH MÌNH
            if (employeeCode == currentUserCode)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "CannotDeleteSelf",
                    Description = "Bạn không thể tự xóa tài khoản của chính mình để tránh khóa hệ thống!"
                });
            }

            // 2. Tìm User trong DB
            var user = await _userManager.FindByNameAsync(employeeCode);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"Không tìm thấy nhân viên có mã {employeeCode}"
                });
            }

            // 3. Thực hiện xóa
            // _userManager.DeleteAsync sẽ tự động xóa user khỏi AspNetUsers, 
            // và xóa các liên kết quyền (AspNetUserRoles) của user đó.
            var result = await _userManager.DeleteAsync(user);

            return result;
        }

        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            // 1. Giải mã Access Token cũ (dù đã hết hạn) để lấy UserId
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null) return null!;

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);

            // 2. Tìm Refresh Token trong DB
            var storedToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == request.RefreshToken && x.UserId == userId);

            // 3. Kiểm tra tính hợp lệ
            if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiresAt <= DateTime.UtcNow)
                return null!; // Token không tồn tại, đã bị thu hồi hoặc hết hạn

            // 4. (Tùy chọn & Khuyên dùng) Thu hồi token cũ (Token Rotation)
            storedToken.IsRevoked = true;

            // 5. Tạo cặp Token mới
            var newAccessToken = await _tokenService.GenerateTokenAsync(user!);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // 6. Lưu Refresh Token mới vào DB
            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId = user!.Id,
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7), // Refresh Token sống 7 ngày
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            });

            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpiration = DateTime.UtcNow.AddMinutes(15)
            };
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!)),
                ValidateLifetime = false // 👈 Bỏ qua kiểm tra hết hạn
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
