using ChatService.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatService.Domain
{
    public class ChatMessage : IMongoModel, IUserResourse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string ChatId { get; set; } = null!;

        [BsonRequired]
        public string Text { get; set; } = null!;
        public string UserId { get; set; }
    }
}