using System.Security.Claims;

namespace WoodCatalog.API.Helpers
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}