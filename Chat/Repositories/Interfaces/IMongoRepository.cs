using ChatService.Domain;

namespace ChatService.Repositories.Interfaces
{
    public interface IMongoRepository<TModel, in TFilter>
        where TModel : class, IMongoModel
    {
        Task<List<TModel>> GetAsync(TFilter filter);
        Task<TModel> GetAsync(int id);
        Task<TModel> CreateAsync(TModel message);
        Task UpdateAsync(int id, TModel message);
        Task RemoveAsync(int id);
    }
}
