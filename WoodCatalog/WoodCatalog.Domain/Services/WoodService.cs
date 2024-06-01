using WoodCatalog.Domain.Models;
using WoodCatalog.Domain.Repositories.Interfaces;
using WoodCatalog.Domain.Services.Interfaces;

namespace WoodCatalog.Domain.Services
{
    public class WoodService : IWoodService
    {
        private readonly IWoodRepository _woodRepository;

        public WoodService(IWoodRepository woodRepository)
        {
            _woodRepository = woodRepository;
        }

        public void AddWood(Wood wood)
        {
            _woodRepository.Add(wood);
        }

        public Wood? DeleteWood(string id)
        {
            return _woodRepository.Delete(id);
        }

        public IEnumerable<Wood> GetAllWoods()
        {
            return _woodRepository.GetAll();
        }

        public Wood? GetWoodById(string id)
        {
            return _woodRepository.GetById(id);
        }

        public Wood? UpdateWood(Wood wood)
        {
            return _woodRepository.Update(wood);
        }
    }
}
