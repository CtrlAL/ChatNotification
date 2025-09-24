using ChatService.Domain;

namespace ChatService.Services.Interfaces
{
    public interface IChatService
    {
        Task ProcessMessageAsync(string connectionId, string? userId, string username, ChatMessage chatMessage);
        Task UserConnectedAsync(string connectionId, string? userId);
        Task UserDisconnectedAsync(string connectionId);
    }
}
