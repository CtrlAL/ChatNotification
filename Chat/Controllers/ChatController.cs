using ChatService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kafka.Interfaces;
using ChatService.Domain.Dto;
using ChatService.Infrastructure.Models.Create;
using ChatService.Infrastructure.Models.Get;
using ChatService.Infrastructure.Models.Get.Response;

namespace ChatService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly IChatMessageRepository _messageRepository;
        private readonly IMessageProducer<MessageSendedDto> _messageProducer;

        public ChatController(IChatMessageRepository messageRepository, 
            IMessageProducer<MessageSendedDto> messageProducer, 
            IChatRepository chatRepository)
        {
            _messageRepository = messageRepository;
            _messageProducer = messageProducer;
            _chatRepository = chatRepository;
        }

        [HttpPost("create-chat")]
        public async Task<ActionResult<CreateResponse>> CreateChat()
        {
            var result = await _chatRepository.CreateAsync(new());

            return Ok(new CreateResponse(result));
        }

        [HttpPost("send-message")]
        public async Task<ActionResult<CreateResponse>> SendMessage([FromBody] ChatMessageModel message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
                return BadRequest("Сообщение не может быть пустым.");

            var entity = ChatMessageModel.FromModel(message);

            var result = await _messageRepository.CreateAsync(entity);

            await _messageProducer.ProduceAsync(
                new MessageSendedDto { 
                    ChatId = result.ChatId,
                    MessageId = result.Id,
                    SendTime = DateTime.UtcNow,
                }, 
                default);

            var resultModel =  GetChatMessageModel.ToModel(result);

            return Ok(new CreateResponse(result));
        }

        [HttpGet("message-list")]
        public async Task<ActionResult<SearchResultResponse<GetChatMessageModel>>> GetMessageList()
        {
            var result = (await _messageRepository.GetAsync(filter: new())).Select(GetChatMessageModel.ToModel).ToList();
            var searchResult = new SearchResultResponse<GetChatMessageModel>(result ?? new List<GetChatMessageModel>());

            return Ok(searchResult);
        }

        [HttpPost("chat-list")]
        public async Task<ActionResult<SearchResultResponse<GetChatModel>>> GetChatList()
        {
            var result = (await _chatRepository.GetAsync(filter: new())).Select(GetChatModel.ToModel).ToList();
            var searchResult = new SearchResultResponse<GetChatModel>(result ?? new List<GetChatModel>());

            return Ok(searchResult);
        }
    }
}