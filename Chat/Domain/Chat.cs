using MongoDB.Bson.Serialization.Attributes;

namespace ChatService.Domain
{
    public class Chat : IMongoModel
    {
        [BsonId]
        public int Id { get; set; }
    }
}
