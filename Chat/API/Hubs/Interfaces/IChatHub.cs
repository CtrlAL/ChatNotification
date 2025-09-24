namespace ChatService.API.Hubs.Interfaces
{
    public interface IChatHub
    {
        Task ReceiveMessage(string username, string message);
        Task UserJoined(string userId, string username);
        Task UserLeft(string userId);
    }
}
