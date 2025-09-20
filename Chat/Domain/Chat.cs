using MongoDB.Bson.Serialization.Attributes;

namespace Chat.Domain
{
    public class Chat
    {
        [BsonId]
        public int Id { get; set; }
        public ICollection<ChatMessage> Chats { get; set; }
    }
}
