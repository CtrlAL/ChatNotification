using ChatService.API.Hubs.Interfaces;
using ChatService.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using ChatService.Services.Interfaces;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;
using Kafka.Interfaces;
using ChatService.Domain.Dto;

namespace ChatService.Services
{
    public class ChatService : IChatService
    {
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;
        private readonly IMessageProducer<MessageSendedDto> _messageProducer;
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IConnectionService _connectionService;

        public ChatService(IHubContext<ChatHub, IChatHub> hubContext, 
            IMessageProducer<MessageSendedDto> messageProducer,
            IChatMessageRepository chatMessageRepository,
            IConnectionService connectionService)
        {
            _hubContext = hubContext;
            _chatMessageRepository = chatMessageRepository;
            _connectionService = connectionService;
            _messageProducer = messageProducer;
        }

        public async Task ProcessMessageAsync(string username, ChatMessage chatMessage)
        {
            var message = await _chatMessageRepository.CreateAsync(chatMessage);

            await _messageProducer.ProduceAsync(new MessageSendedDto
            {
                MessageId = message.Id,
                ChatId = message.ChatId,
                SendTime = DateTime.UtcNow,
            }, default);

            await _hubContext.Clients.All.ReceiveMessage(username, message);
        }

        public async Task UserConnectedAsync(string connectionId, string? userId)
        {
            await _connectionService.AddConnectionAsync(connectionId, userId);
            await _hubContext.Clients.All.UserJoined(userId ?? connectionId, "Anonymous");
        }

        public async Task UserDisconnectedAsync(string connectionId)
        {
            var odd = await _connectionService.RemoveConnectionAsync(connectionId);

            if (odd == 0)
            {
                await _hubContext.Clients.All.UserLeft(connectionId);
            }
        }
    }
}
