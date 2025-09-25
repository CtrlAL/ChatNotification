using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.API.Infrastructure.Models.Get;
using ChatService.API.Infrastructure.Models.Get.Response;
using ChatService.Domain;
using System.Security.Claims;
using ChatService.Domain.Filters;
using ChatService.Policy.Autorizators.Interfaces;

namespace ChatService.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly IChatAutorizator _chatAutorizator;

        public ChatController(IChatRepository chatRepository, IChatAutorizator chatAutorizator)
        {
            _chatRepository = chatRepository;
            _chatAutorizator = chatAutorizator;
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
        public async Task<ActionResult<SearchResultResponse<GetChatModel>>> GetChat([FromRoute] string id)
        {
            var result = await _chatRepository.GetAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var authResult = await _chatAutorizator.AuthorizeAsync(id, User);

            if (!authResult.Succeeded)
            {
                return User.Identity?.IsAuthenticated == true ? Forbid() : Challenge();
            }

            return Ok(GetChatModel.ToModel(result));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<SearchResultResponse<GetChatMessageModel>>> Delete([FromRoute] string id)
        {
            var result = await _chatRepository.GetAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var authResult = await _chatAutorizator.AuthorizeAsync(id, User);

            if (!authResult.Succeeded)
            {
                return User.Identity?.IsAuthenticated == true ? Forbid() : Challenge();
            }

            await _chatRepository.RemoveAsync(id);

            return Ok();
        }
    }
}