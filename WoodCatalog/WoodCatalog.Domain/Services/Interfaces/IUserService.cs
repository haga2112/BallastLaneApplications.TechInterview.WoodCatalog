using WoodCatalog.Domain.Models;

namespace WoodCatalog.Domain.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(string id);
        void Register(User user);
        User? UpdateUser(User user);
        User? DeleteUser(string id);
        (bool success, User? user) LoginUser(string id, string password);

    }
}
