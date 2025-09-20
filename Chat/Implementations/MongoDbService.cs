using ChatService.Configs;
using ChatService.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatService.Implementations
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<ChatMessage> ChatMessages { get; set; }
        public IMongoCollection<Chat> Chats { get; set; }

        public MongoDbService(IMongoDatabase database, IOptions<MongoDbSettings> options)
        {
            _database = database;

            var collectionNames = options.Value.CollectionNames;

            ChatMessages = _database.GetCollection<ChatMessage>(collectionNames["ChatMessages"]);
            Chats = _database.GetCollection<Chat>(collectionNames["Chats"]);
        }
    }
}