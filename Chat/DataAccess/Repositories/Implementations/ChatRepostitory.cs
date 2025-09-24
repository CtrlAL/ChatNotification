using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;
using MongoDB.Driver;

namespace ChatService.DataAccess.Repositories.Implementations
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
