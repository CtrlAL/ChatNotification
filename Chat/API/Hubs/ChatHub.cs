using ChatService.API.Hubs.Interfaces;
using ChatService.API.Infrastructure.Models.Create;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using ChatService.ClaimsExtensions;
using ChatService.Policy.Autorizators.Interfaces;

namespace ChatService.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        private readonly IChatService _chatService;
        private readonly IChatAutorizator _chatAutorizator;
        private readonly IChatRepository _chatRepository;

        public ChatHub(IChatService chatService, 
            IChatAutorizator chatAutorizator, 
            IChatRepository chatRepository)
        {
            _chatService = chatService;
            _chatAutorizator = chatAutorizator;
            _chatRepository = chatRepository;
        }

        private ClaimsPrincipal? User => Context.User;


        public async Task SendMessage(ChatMessageModel message)
        {
            User.GetUserInfo(out var username, out var userId);

            if (!User.CheckTokenContinuity(username, userId))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(message.Text))
                return;

            var authResult = await _chatAutorizator.AuthorizeAsync(message.ChatId, User);

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
    }
}
