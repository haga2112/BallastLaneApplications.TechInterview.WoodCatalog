using System.ComponentModel.DataAnnotations;

namespace WoodCatalog.Domain.Models
{
    public class Wood
    {
        public string Id { get; set; } = $"wood:{Guid.NewGuid().ToString()}";
        
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Quality { get; set; } = string.Empty;
    }
}