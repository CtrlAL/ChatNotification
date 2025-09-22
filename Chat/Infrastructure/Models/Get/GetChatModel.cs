using ChatService.Domain;

namespace ChatService.Infrastructure.Models.Get
{
    public class GetChatModel
    {
        public string Id { get; set; }

        public static Chat FromModel(GetChatModel model)
        {
            return new Chat
            {
                Id = model.Id,
            };
        }
    }
}
