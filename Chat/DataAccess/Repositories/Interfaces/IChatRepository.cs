using ChatService.Domain;

namespace ChatService.DataAccess.Repositories.Interfaces
{
    public interface IChatRepository : IMongoRepository<Chat, object>
    {
    }
}
