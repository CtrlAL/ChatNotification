using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;
using ChatService.Domain.Filters;
using MongoDB.Driver;

namespace ChatService.DataAccess.Repositories.Implementations
{
    public class ChatMessageRepository : MongoRepository<ChatMessage, UserResourseFilter>, IChatMessageRepository
    {
        public ChatMessageRepository(IMongoCollection<ChatMessage> collection) : base(collection)
        {
        }

        protected override FilterDefinition<ChatMessage> BuildFilter(UserResourseFilter filter)
        {
            var builder = Builders<ChatMessage>.Filter;
            var filters = new List<FilterDefinition<ChatMessage>>();

            if (!string.IsNullOrEmpty(filter.UserId))
                filters.Add(builder.Eq(x => x.UserId, filter.UserId));

            return filters.Count == 0
                ? builder.Empty
                : builder.And(filters);
        }
    }
}
