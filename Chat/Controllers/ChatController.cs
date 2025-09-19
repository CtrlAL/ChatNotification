using Chat.Repositories.Interfaces;
using Chat.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatMessageRepository _messageRepository;

        public ChatController(IChatMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
                return BadRequest("Сообщение не может быть пустым.");

            await _messageRepository.CreateAsync(message);

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
