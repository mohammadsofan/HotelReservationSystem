using HotelReservationSystem.Application.Interfaces;
using HotelReservationSystem.Application.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;

namespace HotelReservationSystem.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<TokenService> _logger;
        public TokenService(IOptions<JwtSettings> jwtSettings, ILogger<TokenService> logger) { 
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }
        public string GenerateToken(string userId, string userName, string email, string role)
        {
            _logger.LogInformation("Generating JWT token for user {UserId}, email {Email}, role {Role}", userId, email, role);
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes)
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            _logger.LogInformation("JWT token generated for user {UserId}", userId);
            return token;
        }
    }
}
