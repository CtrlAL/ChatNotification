using ChatService.Domain;
using MongoDB.Bson;

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

        public bool IsValid(out IEnumerable<string> errors)
        {
            var errorList = new List<string>();

            if (!string.IsNullOrWhiteSpace(ChatId) && !ObjectId.TryParse(ChatId, out _))
            {
                errorList.Add("The 'ChatId' field must be a valid 24-character hex ObjectId");
            }

            errors = errorList;
            return !errorList.Any();
        }
    }
}
