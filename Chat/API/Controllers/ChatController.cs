using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.API.Infrastructure.Models.Get;
using ChatService.API.Infrastructure.Models.Get.Response;
using ChatService.Domain;
using System.Security.Claims;
using ChatService.Domain.Filters;

namespace ChatService.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IChatMessageRepository _messageRepository;

        public ChatController(IChatMessageRepository messageRepository, 
            IAuthorizationService authorizationService,
            IChatRepository chatRepository)
        {
            _messageRepository = messageRepository;
            _authorizationService = authorizationService;
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

        [HttpGet("message-list/{chatId}")]
        public async Task<ActionResult<SearchResultResponse<GetChatMessageModel>>> GetMessageList([FromRoute] string chatId)
        {
            var chat = await _messageRepository.GetAsync(chatId);

            var authResult = await _authorizationService.AuthorizeAsync(User, chat, "ChatOwner");

            if (!authResult.Succeeded)
            {
                return User.Identity?.IsAuthenticated == true ? Forbid() : Challenge();
            }

            var result = (await _messageRepository.GetAsync(filter: new())).Select(GetChatMessageModel.ToModel).ToList();
            var searchResult = new SearchResultResponse<GetChatMessageModel>(result ?? new List<GetChatMessageModel>());

            return Ok(searchResult);
        }

        [HttpPost("chat-list")]
        public async Task<ActionResult<SearchResultResponse<GetChatModel>>> GetChatList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Forbid();
            }

            var filter = new UserResourseFilter
            {
                UserId = userId
            };

            var result = (await _chatRepository.GetAsync(filter)).Select(GetChatModel.ToModel).ToList();
            var searchResult = new SearchResultResponse<GetChatModel>(result ?? new List<GetChatModel>());

            return Ok(searchResult);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SearchResultResponse<GetChatMessageModel>>> GetChat([FromRoute] string id)
        {
            var result = await _messageRepository.GetAsync(id);

            var authResult = await _authorizationService.AuthorizeAsync(User, result, "ChatOwner");

            if (!authResult.Succeeded)
            {
                return User.Identity?.IsAuthenticated == true ? Forbid() : Challenge();
            }

            return Ok(GetChatMessageModel.ToModel(result));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SearchResultResponse<GetChatMessageModel>>> Delete([FromRoute] string id)
        {
            var result = await _messageRepository.GetAsync(id);

            var authResult = await _authorizationService.AuthorizeAsync(User, result, "ChatOwner");

            if (!authResult.Succeeded)
            {
                return User.Identity?.IsAuthenticated == true ? Forbid() : Challenge();
            }

            await _messageRepository.RemoveAsync(id);

            return Ok();
        }
    }
}