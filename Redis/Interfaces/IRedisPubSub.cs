namespace Redis.Interfaces
{
    public interface IRedisPubSub
    {
        Task<long> PublishAsync(string channel, string message);
        void Subscribe(string channel, Action<string, string> handler);
    }
}
