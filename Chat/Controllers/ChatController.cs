using ChatService.Repositories.Interfaces;
using ChatService.Domain;
using Microsoft.AspNetCore.Mvc;
using Kafka.Interfaces;
using ChatService.Domain.Dto;

namespace ChatService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatMessageRepository _messageRepository;
        private readonly IMessageProducer<MessageSendedDto> _messageProducer;

        public ChatController(IChatMessageRepository messageRepository, IMessageProducer<MessageSendedDto> messageProducer)
        {
            _messageRepository = messageRepository;
            _messageProducer = messageProducer;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
                return BadRequest("Сообщение не может быть пустым.");

            var result = await _messageRepository.CreateAsync(message);

            await _messageProducer.ProduceAsync(
                new MessageSendedDto { 
                    ChatId = result.ChatId,
                    MessageId = result.Id,
                    SendTime = DateTime.UtcNow,
                }, 
                default);

            return Ok("Сообщение отправлено");
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _messageRepository.GetAsync();
            return Ok(history ?? new List<ChatMessage>());
        }
    }
}