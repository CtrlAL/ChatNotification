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
    }
}
