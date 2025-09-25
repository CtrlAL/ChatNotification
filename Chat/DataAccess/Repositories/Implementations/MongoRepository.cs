using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain.Interfaces;
using MongoDB.Driver;

namespace ChatService.DataAccess.Repositories.Implementations
{
    public abstract class MongoRepository<TModel, TFilter> : IMongoRepository<TModel, TFilter>
        where TModel : class, IMongoModel
    {
        protected readonly IMongoCollection<TModel> _collection;

        public MongoRepository(IMongoCollection<TModel> collection)
        {
            _collection = collection;
        }

        public async Task<TModel> CreateAsync(TModel message)
        {
            await _collection.InsertOneAsync(message);
            return message;
        }

        public async Task<List<TModel>> GetAsync(TFilter filter)
        {
            var dbFilter = BuildFilter(filter);
            var cursor = _collection.Find(dbFilter);

            return await cursor.ToListAsync();
        }

        public async Task<TModel> GetAsync(string id)
        {
            var list = await _collection.FindAsync(x => x.Id == id);

            return list.FirstOrDefault();
        }

        public async Task RemoveAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(string id, TModel message)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, message);
        }

        protected abstract FilterDefinition<TModel> BuildFilter(TFilter filter);
    }
}