using ChatService.Implementations;
using ChatService.Repositories.Interfaces;
using ChatService.Domain;
using MongoDB.Driver;

namespace ChatService.Repositories.Implementations
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly IMongoCollection<ChatMessage> _collection;
        private readonly MongoDbService _dbService;

        public ChatMessageRepository(MongoDbService mongoDbService)
        {
            _dbService = mongoDbService;
            _collection = _dbService.ChatMessages;
        }

        public async Task<ChatMessage> CreateAsync(ChatMessage message)
        {
            await _collection.InsertOneAsync(message);
            return message;
        }

        public async Task<List<ChatMessage>> GetAsync()
        {
            var list = await _collection.FindAsync(x => true);
            return list.ToList();
        }

        public async Task<ChatMessage> GetAsync(int id)
        {
            var list = await _collection.FindAsync(x => x.Id == id);
            return list.FirstOrDefault();
        }

        public async Task RemoveAsync(int id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(int id, ChatMessage message)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, message);
        }
    }
}
