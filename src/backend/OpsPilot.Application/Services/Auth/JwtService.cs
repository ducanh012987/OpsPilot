using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OpsPilot.Domain.Entities.Identity;
using OpsPilot.Infrastructure.Persistence.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OpsPilot.Domain.Services.Auth
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;
        private readonly UserManager<User> _userManager;

        public JwtService(IConfiguration configuration, UserManager<User> userManager)
        {
            _options = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ?? throw new InvalidOperationException("JWT options are not configured");
            _userManager = userManager;
        }

        public async Task<string> GenerateAccessTokenAsync(User user)
        {
            try
            {
                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub,
                        user.Id.ToString()),

                    new(JwtRegisteredClaimNames.UniqueName,
                        user.UserName ?? string.Empty),

                    new(JwtRegisteredClaimNames.Email,
                        user.Email ?? string.Empty),

                    new(ClaimTypes.NameIdentifier,
                        user.Id.ToString()),

                    new(ClaimTypes.Name,
                        user.UserName ?? string.Empty)
                };

                claims.AddRange(
                    roles.Select(role =>
                        new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_options.SecretKey));

                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

                var expires = DateTime.UtcNow.AddMinutes(
                    _options.AccessTokenMinutes);

                var token = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    expires: expires,
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new InvalidOperationException("An error occurred while generating the access token.", ex);
            }
        }

        public string GenerateRefreshToken()
        {
            try
            {
                return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new InvalidOperationException("An error occurred while generating the refresh token.", ex);
            }
        }
    }
}
