using ChatService.API.Hubs.Interfaces;
using ChatService.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using ChatService.Services.Interfaces;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;

namespace ChatService.Services
{
    public class ChatService : IChatService
    {
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;
        private readonly ILogger<ChatService> _logger;
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatService(IHubContext<ChatHub, 
            IChatHub> hubContext, 
            ILogger<ChatService> logger,
            IChatMessageRepository chatMessageRepository)
        {
            _hubContext = hubContext;
            _logger = logger;
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task ProcessMessageAsync(
            string connectionId,
            string? userId,
            string username,
            ChatMessage chatMessage)
        {
            var messageToSave = new ChatMessage
            {
            };
            await _chatMessageRepository.CreateAsync(messageToSave);

            await _hubContext.Clients.All.ReceiveMessage(username, chatMessage.Text);

            _logger.LogInformation("Message sent by {UserId} ({Username}): {Text}",
                userId, username, chatMessage.Text);
        }

        public async Task UserConnectedAsync(string connectionId, string? userId)
        {
            await _hubContext.Clients.All.UserJoined(userId ?? connectionId, "Anonymous");
        }

        public async Task UserDisconnectedAsync(string connectionId)
        {
            await _hubContext.Clients.All.UserLeft(connectionId);
        }
    }
}
