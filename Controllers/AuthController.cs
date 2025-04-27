using Microsoft.AspNetCore.Mvc;
using AuthApi.Data;
using AuthApi.Models;
using AuthApi.Services;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] LoginRequest request)
        {
            if (request.GrantType.ToLower() != "password")
            {
                return BadRequest(new { error = "Unsupported grant type" });
            }

            // Opcional: validar clientId e clientSecret
            if (request.ClientId != "web" || request.ClientSecret != "webpass1")
            {
                return Unauthorized(new { error = "Invalid client credentials" });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized(new { error = "Invalid username or password" });
            }

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new
            {
                access_token = accessToken,
                refresh_token = refreshToken
            });
        }
    }
}
