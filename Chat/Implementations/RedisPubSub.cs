using Chat.Interfaces;
using StackExchange.Redis;

namespace Chat.Implementations
{
    public class RedisPubSub : IRedisPubSub
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPubSub(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<long> PublishAsync(string channel, string message)
        {
            var db = _redis.GetDatabase();
            return await db.PublishAsync(channel, message);
        }

        public void Subscribe(string channel, Action<string, string> handler)
        {
            var subscriber = _redis.GetSubscriber();
            subscriber.Subscribe(channel, (ch, msg) =>
            {
                handler(ch, msg);
            });
        }
    }
}
