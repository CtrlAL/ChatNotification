using ChatService.Domain;
using ChatService.Repositories.Interfaces;
using MongoDB.Driver;

namespace ChatService.Repositories.Implementations
{
    public class ChatRepostitory : MongoRepository<Chat, object>, IChatRepository
    {
        public ChatRepostitory(IMongoCollection<Chat> collection) : base(collection)
        {
        }

        protected override IAsyncCursor<Chat> FilterAsync(IAsyncCursor<Chat> cursor, object filter)
        {
            return base.FilterAsync(cursor, filter);
        }
    }
}
