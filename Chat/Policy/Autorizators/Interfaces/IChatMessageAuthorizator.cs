using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain.Filters;
using ChatService.Domain;

namespace ChatService.Policy.Autorizators.Interfaces
{
    public interface IChatMessageAuthorizator : IUserResourseAuthorizator<IChatMessageRepository, ChatMessage, UserResourseFilter>
    {
    }
}
