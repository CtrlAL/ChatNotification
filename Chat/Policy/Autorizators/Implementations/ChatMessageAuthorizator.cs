using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain.Filters;
using ChatService.Domain;
using Microsoft.AspNetCore.Authorization;
using ChatService.Policy.Autorizators.Interfaces;

namespace ChatService.Policy.Autorizators.Implementations
{
    public class ChatMessageAuthorizator : UserResourseAuthorizator<IChatMessageRepository, ChatMessage, UserResourseFilter>, IChatMessageAuthorizator
    {
        public ChatMessageAuthorizator(IChatMessageRepository repository, IAuthorizationService authorizationService) : base(repository, authorizationService)
        {
        }
    }
}
