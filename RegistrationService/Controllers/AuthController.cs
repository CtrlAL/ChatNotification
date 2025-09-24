using Microsoft.AspNetCore.Mvc;
using RegistrationService.Domain;
using RegistrationService.Services;

namespace RegistrationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IKeyCloakManager _authService;

        public AuthController(IKeyCloakManager authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _authService.RegisterUserAsync(request.Email, request.Username, request.Password);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            await _authService.ResetPasswordAsync(request.Username, request.NewPassword);
            return Ok(new { message = "Password reset successfully" });
        }
    }
}
