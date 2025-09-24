namespace Redis.Interfaces
{
    public interface IRedisCache
    {
        Task<string?> GetStringAsync(string key);
        Task<Dictionary<string, string>?> GetHashAsync(string key);
        Task<List<string>?> GetListAsync(string key);
        Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null);
        Task SetHashAsync(string key, Dictionary<string, string> hashFields);
        Task<long> AddToListAsync(string key, string value);
        Task<bool> RemoveAsync(string key);
        Task<long> RemoveAsync(params string[] keys);
        Task<bool> HashDeleteFieldAsync(string key, string field);
        Task<long> HashDeleteFieldsAsync(string key, params string[] fields);
        Task<long> ListRemoveAsync(string key, string value, long count = 0);
    }
}
