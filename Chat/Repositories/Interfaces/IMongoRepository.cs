using ChatService.Domain;

namespace ChatService.Repositories.Interfaces
{
    public interface IMongoRepository<TModel, in TFilter>
        where TModel : class, IMongoModel
    {
        Task<List<TModel>> GetAsync(TFilter filter);
        Task<TModel> GetAsync(string id);
        Task<TModel> CreateAsync(TModel message);
        Task UpdateAsync(string id, TModel message);
        Task RemoveAsync(string id);
    }
}
