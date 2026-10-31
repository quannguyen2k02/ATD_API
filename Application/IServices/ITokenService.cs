using Domain.Enitties;

namespace Application.IServices
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
