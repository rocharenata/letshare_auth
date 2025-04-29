using Microsoft.AspNetCore.Mvc;
using AuthApi.Data;
using AuthApi.Models;
using AuthApi.Services;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/auth")] //correcao
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
            // Validar entrada
            if (request == null || string.IsNullOrEmpty(request.GrantType) || 
                string.IsNullOrEmpty(request.ClientId) || string.IsNullOrEmpty(request.ClientSecret) || 
                string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { error = "Invalid request payload" });
            }

            // Validar GrantType
            if (request.GrantType.ToLower() != "password")
            {
                return BadRequest(new { error = "Unsupported grant type" });
            }

            // Validar clientId e clientSecret
            var validClientId = Environment.GetEnvironmentVariable("CLIENT_ID") ?? "web";
            var validClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET") ?? "webpass1";

            if (request.ClientId != validClientId || request.ClientSecret != validClientSecret)
            {
                return Unauthorized(new { error = "Invalid client credentials" });
            }

            // Buscar o usuário pelo Username (que é o Email)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Username); // email  para autenticação de usuário

            // Verificar se o usuário existe e se a senha está correta
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized(new { error = "Invalid username or password" });
            }

            // Gerar tokens
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
