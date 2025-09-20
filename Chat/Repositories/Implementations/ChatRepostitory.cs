using ChatService.Domain;
using ChatService.Repositories.Interfaces;
using MongoDB.Driver;

namespace ChatService.Repositories.Implementations
{
    public class ChatRepostitory : MongoRepository<Chat>, IChatRepository
    {
        public ChatRepostitory(IMongoCollection<Chat> collection) : base(collection)
        {
        }
    }
}
