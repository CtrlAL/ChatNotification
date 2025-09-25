using ChatService.API.Hubs.Interfaces;
using ChatService.API.Infrastructure.Models.Create;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Policy;
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
        private readonly IAuthorizationService _authorizationService;
        private readonly IChatRepository _chatRepository;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(ChatMessageModel message)
        {
            var user = Context.User;

            var username = user?.FindFirst("preferred_username")?.Value
                        ?? user?.FindFirst("name")?.Value;

            var userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(message.Text))
                return;


            var chat = await _chatRepository.GetAsync(message.ChatId);

            var authResult = await _authorizationService.AuthorizeAsync(user, chat, PolicyNames.ResourceOwner);

            if (!authResult.Succeeded)
            {
                return;
            }

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
