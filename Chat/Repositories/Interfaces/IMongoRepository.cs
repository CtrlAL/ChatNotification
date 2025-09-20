using ChatService.Domain;

namespace ChatService.Repositories.Interfaces
{
    public interface IMongoRepository<TModel>
        where TModel : class, IMongoModel
    {
        Task<List<TModel>> GetAsync();
        Task<TModel> GetAsync(int id);
        Task<TModel> CreateAsync(TModel message);
        Task UpdateAsync(int id, TModel message);
        Task RemoveAsync(int id);
    }
}
