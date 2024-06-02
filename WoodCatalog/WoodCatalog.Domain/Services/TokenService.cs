using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WoodCatalog.Domain.Models;
using WoodCatalog.Domain.Services.Interfaces;

namespace UserCatalog.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim> {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.Name),
            };

            var secret = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var key = new SymmetricSecurityKey(secret);
            var signature = SecurityAlgorithms.HmacSha256Signature;

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(key, signature));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string ValidateToken(string token)
        {
            if (token == null)
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return string.Empty;
            }
        }
    }
}
