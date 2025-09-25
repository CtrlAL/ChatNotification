using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain.Filters;
using ChatService.Domain;
using Microsoft.AspNetCore.Authorization;
using ChatService.Policy.Autorizators.Interfaces;

namespace ChatService.Policy.Autorizators.Implementations
{
    public class ChatAutorizator : UserResourseAuthorizator<IChatRepository, Chat, UserResourseFilter>, IChatAutorizator
    {
        public ChatAutorizator(IChatRepository repository, IAuthorizationService authorizationService) : base(repository, authorizationService)
        {
        }
    }
}
