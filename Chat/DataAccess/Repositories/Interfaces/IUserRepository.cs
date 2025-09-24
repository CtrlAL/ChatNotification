using ChatService.Domain;

namespace ChatService.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IMongoRepository<User, object>
    {
        public Task<User> GetBySubId(string subId);
    }
}
