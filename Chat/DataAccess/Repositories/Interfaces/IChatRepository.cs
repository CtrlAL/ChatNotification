using ChatService.Domain;
using ChatService.Domain.Filters;

namespace ChatService.DataAccess.Repositories.Interfaces
{
    public interface IChatRepository : IMongoRepository<Chat, UserResourseFilter>
    {
    }
}
