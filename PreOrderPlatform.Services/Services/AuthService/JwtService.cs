using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace PreOrderPlatform.Service.Services.AuthService
{
    public class JwtService : IJwtService
    {
        private readonly string _jwtSecret;
        private readonly double _expirationInHours;

        public JwtService(IConfiguration configuration)
        {
            _jwtSecret = configuration.GetSection("Jwt:Secret").Value;
            _expirationInHours = Double.Parse(configuration.GetSection("Jwt:ExpirationInHours").Value);
        }

        public string GenerateToken(string userId, string userName, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role),
            }),
                Expires = DateTime.UtcNow.AddHours(_expirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Other methods will be added here
    }
}
