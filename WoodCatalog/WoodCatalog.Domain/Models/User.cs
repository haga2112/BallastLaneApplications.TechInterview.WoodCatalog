using System.ComponentModel.DataAnnotations;

namespace WoodCatalog.Domain.Models
{
    public class User
    {
        public string Id { get; set; } = $"user:{Guid.NewGuid().ToString()}";

        [Required]
        public required string Name { get; set; } = string.Empty;
        
        [Required]
        public required string Password { get; set; } = string.Empty;

    }
}