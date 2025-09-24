using ChatService.API.Hubs.Interfaces;
using ChatService.Domain;
using ChatService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(ChatMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
                return;

            var username = Context.User?.FindFirst("preferred_username")?.Value
                        ?? Context.User?.FindFirst("name")?.Value
                        ?? "Anonymous";

            await _chatService.ProcessMessageAsync(Context.ConnectionId, Context.UserIdentifier, username, message);
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            await _chatService.UserConnectedAsync(Context.ConnectionId, Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _chatService.UserDisconnectedAsync(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
