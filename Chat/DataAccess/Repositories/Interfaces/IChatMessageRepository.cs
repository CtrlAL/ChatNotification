using ChatService.Domain;
using ChatService.Domain.Filters;

namespace ChatService.DataAccess.Repositories.Interfaces
{
    public interface IChatMessageRepository : IMongoRepository<ChatMessage, UserResourseFilter>
    {
    }
}
