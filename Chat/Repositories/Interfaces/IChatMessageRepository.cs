using ChatService.Domain;

namespace ChatService.Repositories.Interfaces
{
    public interface IChatMessageRepository : IMongoRepository<ChatMessage, object>
    {
    }
}
