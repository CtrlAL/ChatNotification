using MongoDB.Bson.Serialization.Attributes;

namespace ChatService.Domain
{
    public class Chat
    {
        [BsonId]
        public int Id { get; set; }
    }
}
