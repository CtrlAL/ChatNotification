using ChatService.Domain;

namespace ChatService.DataAccess.Repositories.Interfaces
{
    public interface IChatMessageRepository : IMongoRepository<ChatMessage, object>
    {
    }
}
