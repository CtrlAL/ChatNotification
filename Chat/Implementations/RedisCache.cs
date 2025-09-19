using Chat.Interfaces;
using StackExchange.Redis;

namespace Chat.Implementations
{
    public class RedisCache : IRedisCache
    {
        private readonly IDatabase _db;

        public RedisCache(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
        }

        public async Task<string?> GetStringAsync(string key)
        {
            var result = await _db.StringGetAsync(key);
            return result.HasValue ? result.ToString() : null;
        }

        public async Task<Dictionary<string, string>?> GetHashAsync(string key)
        {
            var hash = await _db.HashGetAllAsync(key);
            if (hash.Length == 0) return null;

            return hash.ToDictionary(
                entry => entry.Name.ToString(),
                entry => entry.Value.ToString()
            );
        }

        public async Task<List<string>?> GetListAsync(string key)
        {
            var list = await _db.ListRangeAsync(key);
            if (list.Length == 0) return null;

            return list.Select(x => x.ToString()).ToList();
        }

        public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null)
        {
            return await _db.StringSetAsync(key, value, expiry);
        }

        public Task SetHashAsync(string key, Dictionary<string, string> hashFields)
        {
            var entries = hashFields
                .Select(kvp => new HashEntry(kvp.Key, kvp.Value))
                .ToArray();

            return _db.HashSetAsync(key, entries);
        }

        public async Task<long> AddToListAsync(string key, string value)
        {
            return await _db.ListRightPushAsync(key, value);
        }
    }
}
