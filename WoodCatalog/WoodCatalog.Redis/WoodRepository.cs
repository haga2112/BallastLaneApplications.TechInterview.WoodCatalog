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
    public class WoodRepository : IWoodRepository
    {
        private readonly IConnectionMultiplexer _redis;
        public WoodRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void Add(Wood wood)
        {
            if (wood == null)
            {
                throw new ArgumentOutOfRangeException(nameof(wood));
            }
            var db = _redis.GetDatabase();
            string serializedWood = JsonSerializer.Serialize(wood);
            db.StringSet(wood.Id, serializedWood);
        }

        public Wood? Delete(string id)
        {
            var db = _redis.GetDatabase();
            var wood = db.StringGet(id);
            if (wood.IsNullOrEmpty)
            {
                return null;
            }
            db.KeyDelete(id);
            return JsonSerializer.Deserialize<Wood>(wood!);
        }

        public IEnumerable<Wood> GetAll()
        {
            var db = _redis.GetDatabase();

            var woodKeys = db.Multiplexer.GetServer(_redis.GetEndPoints().First()).Keys(pattern: "wood:*");

            var woods = new List<Wood>();

            foreach (var key in woodKeys)
            {
                var woodJson = db.StringGet(key);
                if (!woodJson.IsNullOrEmpty)
                {
                    Wood? wood = JsonSerializer.Deserialize<Wood>(woodJson!);
                    woods.Add(wood!);
                }
            }

            return woods;
        }

        public Wood? GetById(string id)
        {
            var db = _redis.GetDatabase();
            var wood = db.StringGet(id);
            if (wood.IsNullOrEmpty)
            {
                return null;
            }
            return JsonSerializer.Deserialize<Wood>(wood!);
        }

        public Wood? Update(Wood wood)
        {
            var db = _redis.GetDatabase();

            if (db.KeyExists(wood.Id))
            {
                var updatedWoodJson = JsonSerializer.Serialize(wood);
                db.StringSet(wood.Id, updatedWoodJson);
                return wood;
            }
            else
            {
                return null;
            }
        }
    }
}
