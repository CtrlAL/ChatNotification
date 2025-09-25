using ChatService.API.Hubs.Interfaces;
using ChatService.API.Infrastructure.Models.Create;
using ChatService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

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

        public async Task SendMessage(ChatMessageModel message)
        {
            var username = Context.User?.FindFirst("preferred_username")?.Value
                        ?? Context.User?.FindFirst("name")?.Value;

            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(username))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(message.Text))
                return;

            var entity = ChatMessageModel.FromModel(message, userId);

            await _chatService.ProcessMessageAsync(username, entity);
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
