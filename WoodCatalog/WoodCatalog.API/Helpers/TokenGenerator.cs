using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WoodCatalog.Domain.Models;

namespace WoodCatalog.API.Helpers
{
    public class TokenGenerator
    {
        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim> {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.Name),
            };

            var secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ApplicationSettings:JWT_Secret")!);
            var key = new SymmetricSecurityKey(secret);
            var signature = SecurityAlgorithms.HmacSha256Signature;

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(key, signature));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
