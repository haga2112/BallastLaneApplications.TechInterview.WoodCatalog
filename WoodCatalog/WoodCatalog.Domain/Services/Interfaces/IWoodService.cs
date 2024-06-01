using WoodCatalog.Domain.Models;

namespace WoodCatalog.Domain.Services.Interfaces
{
    public interface IWoodService
    {
        IEnumerable<Wood> GetAllWoods();
        Wood? GetWoodById(string id);
        void AddWood(Wood wood);
        Wood? UpdateWood(Wood wood);
        Wood? DeleteWood(string id);
    }
}
