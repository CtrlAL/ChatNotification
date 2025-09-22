using ChatService.Domain;

namespace ChatService.Infrastructure.Models.Create
{
    public class ChatMessageModel
    {
        public string ChatId { get; set; }
        public string Text { get; set; }

        public static ChatMessage FromModel(ChatMessageModel model)
        {
            return new ChatMessage
            {
                ChatId = model.ChatId,
                Text = model.Text,
            };
        }
    }
}
