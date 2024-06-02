using WoodCatalog.Domain.Models;

namespace WoodCatalog.Domain.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);

        string ValidateToken(string token);
    }
}
