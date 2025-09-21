using ChatService.Domain;

namespace ChatService.Repositories.Interfaces
{
    public interface IChatRepository : IMongoRepository<Chat, object>
    {
    }
}
