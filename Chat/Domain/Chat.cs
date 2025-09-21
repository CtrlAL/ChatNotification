using MongoDB.Bson.Serialization.Attributes;

namespace ChatService.Domain
{
    public class Chat : IMongoModel
    {
        [BsonId]
        public string Id { get; set; }
    }
}
