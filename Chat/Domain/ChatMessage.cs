using MongoDB.Bson.Serialization.Attributes;

namespace ChatService.Domain
{
    public class ChatMessage
    {
        [BsonId]
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Text {  get; set; }
    }
}
