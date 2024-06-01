namespace WoodCatalog.Domain.Models
{
    public class User
    {
        public string Id { get; set; } = $"wood:{Guid.NewGuid().ToString()}";
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;

    }
}