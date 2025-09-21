using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatService.Domain
{
    public class Chat : IMongoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
    }
}
