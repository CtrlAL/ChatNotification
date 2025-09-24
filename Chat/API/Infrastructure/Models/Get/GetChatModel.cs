using ChatService.Domain;

namespace ChatService.API.Infrastructure.Models.Get
{
    public class GetChatModel
    {
        public string Id { get; set; }

        public static GetChatModel ToModel(Chat entity)
        {
            return new GetChatModel
            {
                Id = entity.Id,
            };
        }
    }
}
