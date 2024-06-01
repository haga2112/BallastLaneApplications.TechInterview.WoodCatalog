using WoodCatalog.Domain.Models;

namespace WoodCatalog.Domain.Repositories.Interfaces
{
    public interface IWoodRepository
    {
        IEnumerable<Wood> GetAll();
        Wood? GetById(string id);
        void Add(Wood wood);
        Wood? Update(Wood wood);
        Wood? Delete(string id);
    }
}