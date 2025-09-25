using MongoDB.Bson;

namespace ChatService.Domain.Interfaces
{
    public interface IMongoModel
    {
        public string Id { get; set; }
    }
}