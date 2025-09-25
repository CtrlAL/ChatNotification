using ChatService.Domain;

namespace ChatService.API.Infrastructure.Models.Create
{
    public class ChatMessageModel
    {
        public string ChatId { get; set; }
        public string Text { get; set; }

        public static ChatMessage FromModel(ChatMessageModel model, string userId)
        {
            return new ChatMessage
            {
                UserId = userId,
                ChatId = model.ChatId,
                Text = model.Text,
            };
        }
    }
}
