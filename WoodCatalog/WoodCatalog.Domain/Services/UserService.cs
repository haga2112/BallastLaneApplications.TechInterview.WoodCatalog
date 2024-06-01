using Microsoft.Extensions.Logging;
using WoodCatalog.Domain.Models;
using WoodCatalog.Domain.Repositories.Interfaces;
using WoodCatalog.Domain.Services.Interfaces;

namespace UserCatalog.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger,
                            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        public User? DeleteUser(string id)
        {
            return _userRepository.Delete(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User? GetUserById(string id)
        {
            return _userRepository.GetById(id);
        }

        public User? UpdateUser(User user)
        {
            return _userRepository.Update(user);
        }
    
        public (bool success, User? user) LoginUser(string id, string password)
        {
            User? user = _userRepository.GetById(id);

            if (user == null) {
                // TODO: validate and log "User not found";
                return (false, null);
            }

            if (user.Password == password)
            {
                return (true, user);
            }
            else
            {
                // TODO: validate and log "Wrong password";
                return (false, null);
            }
        }
    }
}
