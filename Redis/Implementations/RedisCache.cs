using Redis.Interfaces;
using StackExchange.Redis;

namespace Redis.Implementations
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

        public async Task<bool> RemoveAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        public async Task<long> RemoveAsync(params string[] keys)
        {
            var redisKeys = keys.Select(k => (RedisKey)k).ToArray();
            return await _db.KeyDeleteAsync(redisKeys);
        }

        public async Task<bool> HashDeleteFieldAsync(string key, string field)
        {
            return await _db.HashDeleteAsync(key, field);
        }

        public async Task<long> HashDeleteFieldsAsync(string key, params string[] fields)
        {
            var redisFields = fields.Select(f => (RedisValue)f).ToArray();
            return await _db.HashDeleteAsync(key, redisFields);
        }

        public async Task<long> ListRemoveAsync(string key, string value, long count = 0)
        {
            return await _db.ListRemoveAsync(key, value, count);
        }
    }
}
