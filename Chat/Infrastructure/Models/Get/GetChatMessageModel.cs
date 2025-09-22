using ChatService.Domain;

namespace ChatService.Infrastructure.Models.Get
{
    public class GetChatMessageModel
    {
        public string Id { get; set; }
        public string ChatId { get; set; }
        public string Text { get; set; }

        public static GetChatMessageModel ToModel(ChatMessage entity)
        {
            return new GetChatMessageModel
            {
                Id = entity.Id,
                ChatId = entity.ChatId,
                Text = entity.Text,
            };
        }

        public static ChatMessage FromModel(GetChatMessageModel model)
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
