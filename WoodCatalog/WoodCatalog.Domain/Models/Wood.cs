namespace WoodCatalog.Domain.Models
{
    public class Wood
    {
        public string Id { get; set; } = $"wood:{Guid.NewGuid().ToString()}";
        public required string Name { get; set; } = string.Empty;
    }
}
