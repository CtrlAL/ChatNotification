using ChatService.Domain;

namespace ChatService.API.Hubs.Interfaces
{
    public interface IChatHub
    {
        Task ReceiveMessage(string username, ChatMessage message);
        Task UserJoined(string userId, string username);
        Task UserLeft(string userId);
    }
}
