using KafkaTest.Domain;
using KafkaTest.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KafkaTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IRedisPubSub _redis;
        private readonly IRedisCache _redisCache;

        public ChatController(IRedisPubSub redis, IRedisCache redisCache)
        {
            _redis = redis;
            _redisCache = redisCache;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
                return BadRequest("Сообщение не может быть пустым.");

            var formattedMessage = $"{DateTime.Now:HH:mm:ss} [{message.User}]: {message.Text}";

            await _redisCache.AddToListAsync("chat:history", formattedMessage);

            await _redis.PublishAsync("chat", formattedMessage);

            return Ok("Сообщение отправлено");
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _redisCache.GetListAsync("chat:history");
            return Ok(history ?? new List<string>());
        }
    }
}
