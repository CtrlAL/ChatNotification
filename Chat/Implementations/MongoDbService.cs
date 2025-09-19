using Chat.Configs;
using Chat.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chat.Implementations
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<ChatMessage> ChatMessages { get; set; }

        public MongoDbService(IMongoDatabase database, IOptions<MongoDbSettings> options)
        {
            _database = database;
            SetupCollections(options);
        }

        private void SetupCollections(IOptions<MongoDbSettings> options)
        {
            var collectionNames = options.Value.CollectionNames;
            ChatMessages = _database.GetCollection<ChatMessage>(collectionNames["ChatMessages"]);
        }
    }
}