using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;
using MongoDB.Driver;

namespace ChatService.DataAccess.Repositories.Implementations
{
    public class ChatMessageRepository : MongoRepository<ChatMessage, object>, IChatMessageRepository
    {
        public ChatMessageRepository(IMongoCollection<ChatMessage> collection) : base(collection)
        {
        }

        protected override IAsyncCursor<ChatMessage> FilterAsync(IAsyncCursor<ChatMessage> cursor, object filter)
        {
            return base.FilterAsync(cursor, filter);
        }
    }
}
