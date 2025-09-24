using ChatService.Services.Interfaces;
using Redis.Interfaces;

namespace ChatService.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IRedisCache _cache;

        public ConnectionService(IRedisCache cache)
        {
            _cache = cache;
        }

        public async Task AddConnectionAsync(string userId, string connectionId)
        {
            await _cache.AddToListAsync($"user:{userId}:connections", connectionId);
            await _cache.SetStringAsync($"connection:{connectionId}:user", userId);
        }

        public async Task<int> RemoveConnectionAsync(string connectionId)
        {
            var userId = await _cache.GetStringAsync($"connection:{connectionId}:user");
            if (userId == null) return 0;

            await _cache.ListRemoveAsync($"user:{userId}:connections", connectionId);

            await _cache.RemoveAsync($"connection:{connectionId}:user");

            var connections = await _cache.GetListAsync($"user:{userId}:connections");

            return connections?.Count ?? 0;
        }
    }
}
