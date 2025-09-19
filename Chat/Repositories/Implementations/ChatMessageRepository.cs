using Chat.Implementations;
using Chat.Repositories.Interfaces;
using KafkaTest.Domain;
using MongoDB.Driver;

namespace Chat.Repositories.Implementations
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

        public async Task<ChatMessage> Create(ChatMessage message)
        {
            await _collection.InsertOneAsync(message);
            return message;
        }

        public async Task<List<ChatMessage>> Get()
        {
            var list = await _collection.FindAsync(x => true);
            return list.ToList();
        }

        public async Task<ChatMessage> Get(int id)
        {
            var list = await _collection.FindAsync(x => x.Id == id);
            return list.FirstOrDefault();
        }

        public async Task Remove(int id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task Update(int id, ChatMessage message)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, message);
        }
    }
}
