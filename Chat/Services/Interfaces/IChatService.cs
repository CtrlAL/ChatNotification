using ChatService.Domain;

namespace ChatService.Services.Interfaces
{
    public interface IChatService
    {
        Task ProcessMessageAsync(string username, ChatMessage chatMessage);
        Task UserConnectedAsync(string connectionId, string userId, string username);
        Task UserDisconnectedAsync(string connectionId, string userId, string username);
    }
}
