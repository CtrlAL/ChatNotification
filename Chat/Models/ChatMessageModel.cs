using ChatService.Domain;

namespace ChatService.Models
{
    public class ChatMessageModel
    {
        public string? Id { get; set; }
        public string ChatId { get; set; }
        public string Text { get; set; }

        public static ChatMessageModel ToModel(ChatMessage entity)
        {
            return new ChatMessageModel
            {
                Id = entity.Id,
                ChatId = entity.ChatId,
                Text = entity.Text,
            };
        }

        public static ChatMessage FromModel(ChatMessageModel model)
        {
            return new ChatMessage
            {
                Id = model.Id,
                ChatId = model.ChatId,
                Text = model.Text,
            };
        }
    }
}
