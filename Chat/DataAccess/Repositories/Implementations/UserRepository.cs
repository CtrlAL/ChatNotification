using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;
using MongoDB.Driver;

namespace ChatService.DataAccess.Repositories.Implementations
{
    public class UserRepository : MongoRepository<User, object>, IUserRepository
    {
        public UserRepository(IMongoCollection<User> collection) : base(collection)
        {
        }

        public async Task<User> GetBySubId(string subId)
        {
            var users = await _collection.FindAsync(x => x.SubId == subId);
            return users.FirstOrDefault();
        }

        protected override IAsyncCursor<User> FilterAsync(IAsyncCursor<User> cursor, object filter)
        {
            return base.FilterAsync(cursor, filter);
        }
    }
}
