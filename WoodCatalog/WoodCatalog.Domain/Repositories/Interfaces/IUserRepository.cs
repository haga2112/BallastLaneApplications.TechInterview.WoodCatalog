using WoodCatalog.Domain.Models;

namespace WoodCatalog.Domain.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(string id);
        void Add(User user);
        User? Update(User user);
        User? Delete(string id);
    }
}