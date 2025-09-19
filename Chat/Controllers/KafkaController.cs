using Chat.Domain;
using Chat.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly IMessageProducer<Message> _messageProducer;

        public KafkaController(IMessageProducer<Message> messageProducer)
        {
            _messageProducer = messageProducer;
        }

        [HttpPost(Name = "SendMessage")]
        public async Task<IActionResult> Get(Message message)
        {
            await _messageProducer.ProduceAsync(message, default);

            return Ok();
        }
    }
}