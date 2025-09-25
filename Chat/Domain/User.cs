using ChatService.Domain.Interfaces;

namespace ChatService.Domain
{
    public class User : IMongoModel
    {
        public string Id { get; set; }

        public string SubId { get; set; }

        public string TelegramName {  get; set; }
    }
}
