using Chat.Configs;
using KafkaTest.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chat.Implementations
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Message> Messages { get; set; }

        public MongoDbService(IMongoDatabase database, IOptions<MongoDbSettings> options)
        {
            _database = database;
            SetupCollections(options);
        }

        private void SetupCollections(IOptions<MongoDbSettings> options)
        {
            var collectionNames = options.Value.CollectionNames;
            Messages = _database.GetCollection<Message>(collectionNames["Messages"]);
        }

        public IMongoDatabase Database => _database;
    }
}
