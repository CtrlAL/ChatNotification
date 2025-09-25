using Microsoft.AspNetCore.Mvc;
using Kafka.Interfaces;
using ChatService.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.API.Infrastructure.Models.Create;
using ChatService.API.Infrastructure.Models.Get;
using ChatService.API.Infrastructure.Models.Get.Response;
using ChatService.Domain;
using System.Security.Claims;

namespace ChatService.API.Controllers
{
    [ApiController]
    [Authorize]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Forbid();
            }

            var chat = new Chat
            {
                UserId = userId
            };

            var result = await _chatRepository.CreateAsync(chat);

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