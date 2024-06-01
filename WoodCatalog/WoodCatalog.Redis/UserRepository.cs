using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WoodCatalog.Domain.Models;
using WoodCatalog.Domain.Repositories.Interfaces;

namespace WoodCatalog.Redis
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionMultiplexer _redis;
        public UserRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(user));
            }
            var db = _redis.GetDatabase();
            string serializedUser = JsonSerializer.Serialize(user);
            db.StringSet(user.Id, serializedUser);
        }

        public User? Delete(string id)
        {
            var db = _redis.GetDatabase();
            var user = db.StringGet(id);
            if (user.IsNullOrEmpty)
            {
                return null;
            }
            db.KeyDelete(id);
            return JsonSerializer.Deserialize<User>(user!);
        }

        public IEnumerable<User> GetAll()
        {
            var db = _redis.GetDatabase();

            var userKeys = db.Multiplexer.GetServer(_redis.GetEndPoints().First()).Keys(pattern: "user:*");

            var users = new List<User>();

            foreach (var key in userKeys)
            {
                var userJson = db.StringGet(key);
                if (!userJson.IsNullOrEmpty)
                {
                    User? user = JsonSerializer.Deserialize<User>(userJson!);
                    users.Add(user!);
                }
            }

            return users;
        }

        public User? GetById(string id)
        {
            var db = _redis.GetDatabase();
            var user = db.StringGet(id);
            if (user.IsNullOrEmpty)
            {
                return null;
            }
            return JsonSerializer.Deserialize<User>(user!);
        }
        public User? Update(User user)
        {
            var db = _redis.GetDatabase();

            if (db.KeyExists(user.Id))
            {
                var updatedUserJson = JsonSerializer.Serialize(user);
                db.StringSet(user.Id, updatedUserJson);
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
