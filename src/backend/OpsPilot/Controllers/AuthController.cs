using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpsPilot.Application.Applications.DTOs.Auth;
using OpsPilot.Domain.Services.Auth;

namespace OpsPilot.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> Register(RegisterRequest request)
        {
            return Ok(
                await _authService.RegisterAsync(request));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            return Ok(
                await _authService.LoginAsync(request));
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponse>> Refresh(RefreshTokenRequest request)
        {
            return Ok(await _authService.RefreshTokenAsync(request.RefreshToken));
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,

                UserName = User.Identity?.Name,

                Roles = User.Claims
                    .Where(x => x.Type == System.Security.Claims.ClaimTypes.Role)
                    .Select(x => x.Value)
            });
        }
    }
}
