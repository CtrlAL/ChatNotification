using ChatService.API.Hubs.Interfaces;
using ChatService.API.Infrastructure.Models.Create;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Policy;
using ChatService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using ChatService.ClaimsExtensions;
using ChatService.Domain;

namespace ChatService.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        private readonly IChatService _chatService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IChatRepository _chatRepository;

        private ClaimsPrincipal? User => Context.User;

        public ChatHub(IChatService chatService, 
            IAuthorizationService authorizationService, 
            IChatRepository chatRepository)
        {
            _chatService = chatService;
            _authorizationService = authorizationService;
            _chatRepository = chatRepository;
        }

        public async Task SendMessage(ChatMessageModel message)
        {
            User.GetUserInfo(out var username, out var userId);

            if (!User.CheckTokenContinuity(username, userId))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(message.Text))
                return;

            var authResult = await AuthChatResourse(message.ChatId, User);

            if (!authResult.Succeeded)
            {
                return;
            }

            var entity = ChatMessageModel.FromModel(message, userId);

            await _chatService.ProcessMessageAsync(username, entity);
        }

        public override async Task OnConnectedAsync()
        {
            User.GetUserInfo(out var username, out var userId);

            if (!User.CheckTokenContinuity(username, userId))
            {
                return;
            }

            await _chatService.UserConnectedAsync(Context.ConnectionId, userId, username);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            User.GetUserInfo(out var username, out var userId);

            if (!User.CheckTokenContinuity(username, userId))
            {
                return;
            }

            await _chatService.UserDisconnectedAsync(Context.ConnectionId, userId, username);

            await base.OnDisconnectedAsync(exception);
        }

        private async Task<AuthorizationResult> AuthChatResourse(string chatId, ClaimsPrincipal user)
        {
            var chat = await _chatRepository.GetAsync(chatId);

            return await _authorizationService.AuthorizeAsync(user, chat, PolicyNames.ResourceOwner);
        }
    }
}
