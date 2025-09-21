using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatService.Domain
{
    public class ChatMessage : IMongoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string ChatId { get; set; } = null!;

        [BsonRequired]
        public string Text { get; set; } = null!;
    }
}