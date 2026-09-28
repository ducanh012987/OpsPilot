using OpsPilot.Domain.Entities.Identity;

namespace OpsPilot.Domain.Services.Auth
{
    public interface IJwtService
    {
        Task<string> GenerateAccessTokenAsync(User user);

        string GenerateRefreshToken();
    }
}
