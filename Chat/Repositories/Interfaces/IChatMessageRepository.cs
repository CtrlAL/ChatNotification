using KafkaTest.Domain;

namespace Chat.Repositories.Interfaces
{
    public interface IChatMessageRepository
    {
        Task<List<ChatMessage>> Get();
        Task<ChatMessage> Get(int id);
        Task<ChatMessage> Create(ChatMessage message);
        Task Update(int id, ChatMessage message);
        Task Remove(int id);
    }
}
