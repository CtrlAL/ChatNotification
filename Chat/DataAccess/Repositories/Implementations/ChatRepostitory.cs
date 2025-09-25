using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;
using ChatService.Domain.Filters;
using MongoDB.Driver;

namespace ChatService.DataAccess.Repositories.Implementations
{
    public class ChatRepostitory : MongoRepository<Chat, UserResourseFilter>, IChatRepository
    {
        public ChatRepostitory(IMongoCollection<Chat> collection) : base(collection)
        {
        }

        protected override FilterDefinition<Chat> BuildFilter(UserResourseFilter filter)
        {
            var builder = Builders<Chat>.Filter;
            var filters = new List<FilterDefinition<Chat>>();

            if (!string.IsNullOrEmpty(filter.UserId))
                filters.Add(builder.Eq(x => x.UserId, filter.UserId));

            return filters.Count == 0
                ? builder.Empty
                : builder.And(filters);
        }
    }
}