using OpsPilot.Application.Applications.DTOs.Auth;

namespace OpsPilot.Domain.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse> RegisterAsync(RegisterRequest request);

        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task<LoginResponse> RefreshTokenAsync(string refreshToken);
    }
}
