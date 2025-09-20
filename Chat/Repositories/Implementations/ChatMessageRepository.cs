using ChatService.Domain;
using ChatService.Repositories.Interfaces;
using MongoDB.Driver;

namespace ChatService.Repositories.Implementations
{
    public class ChatMessageRepository : MongoRepository<ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(IMongoCollection<ChatMessage> collection) : base(collection)
        {
        }
    }
}
