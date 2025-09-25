using ChatService.API.Infrastructure.Models.Get.Response;
using ChatService.API.Infrastructure.Models.Get;
using ChatService.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChatService.Policy.Autorizators.Interfaces;

namespace ChatService.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IChatMessageRepository _messageRepository;
        private readonly IChatAutorizator _chatAutorizator;
        private readonly IChatMessageAuthorizator _chatMessageAuthorizator;

        public MessageController(IChatMessageRepository messageRepository, 
            IChatAutorizator chatAutorizator, 
            IChatMessageAuthorizator chatMessageAuthorizator)
        {
            _messageRepository = messageRepository;
            _chatAutorizator = chatAutorizator;
            _chatMessageAuthorizator = chatMessageAuthorizator;
        }

        [HttpGet("message-list/{chatId}")]
        public async Task<ActionResult<SearchResultResponse<GetChatMessageModel>>> GetMessageList([FromRoute] string chatId)
        {
            var authResult = await _chatAutorizator.AuthorizeAsync(chatId, User);

            if (!authResult.Succeeded)
            {
                return User.Identity?.IsAuthenticated == true ? Forbid() : Challenge();
            }

            var result = (await _messageRepository.GetAsync(filter: new())).Select(GetChatMessageModel.ToModel).ToList();
            var searchResult = new SearchResultResponse<GetChatMessageModel>(result ?? new List<GetChatMessageModel>());

            return Ok(searchResult);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SearchResultResponse<GetChatMessageModel>>> Delete([FromRoute] string id)
        {
            var result = await _messageRepository.GetAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            var authResult = await _chatMessageAuthorizator.AuthorizeAsync(result, User);

            if (!authResult.Succeeded)
            {
                return User.Identity?.IsAuthenticated == true ? Forbid() : Challenge();
            }

            await _messageRepository.RemoveAsync(id);

            return Ok();
        }
    }
}
