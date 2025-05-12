using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductAPI.Helpers
{
    public class TokenHelper(IConfiguration config)
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = new();
        private readonly SymmetricSecurityKey _signingKey = new(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));

        public string GetJwtToken(int userId)
        {
            return GenerateJwtToken(userId);
        }

        private string GenerateJwtToken(int userId)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            int expirationInMinutes = int.Parse(config["Jwt:ExpirationInMinutes"] ?? "10");

            var description = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = config["Jwt:Issuer"],
                Audience = config["Jwt:Audience"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                SigningCredentials = credentials
            };

            var token = _tokenHandler.CreateToken(description);
            return _tokenHandler.WriteToken(token);
        }
    }
}
