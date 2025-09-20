using ChatService.Domain;
using ChatService.Repositories.Interfaces;
using MongoDB.Driver;

namespace ChatService.Repositories.Implementations
{
    public class MongoRepository<TModel> : IMongoRepository<TModel>
        where TModel : class, IMongoModel
    {
        private readonly IMongoCollection<TModel> _collection;

        public MongoRepository(IMongoCollection<TModel> collection)
        {
            _collection = collection;
        }

        public async Task<TModel> CreateAsync(TModel message)
        {
            await _collection.InsertOneAsync(message);
            return message;
        }

        public async Task<List<TModel>> GetAsync()
        {
            var list = await _collection.FindAsync(x => true);
            return list.ToList();
        }

        public async Task<TModel> GetAsync(int id)
        {
            var list = await _collection.FindAsync(x => x.Id == id);
            return list.FirstOrDefault();
        }

        public async Task RemoveAsync(int id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(int id, TModel message)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, message);
        }
    }
}
