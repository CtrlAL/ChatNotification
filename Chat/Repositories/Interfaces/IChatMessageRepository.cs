using KafkaTest.Domain;

namespace Chat.Repositories.Interfaces
{
    public interface IChatMessageRepository
    {
        Task<List<ChatMessage>> GetAsync();
        Task<ChatMessage> GetAsync(int id);
        Task<ChatMessage> CreateAsync(ChatMessage message);
        Task UpdateAsync(int id, ChatMessage message);
        Task RemoveAsync(int id);
    }
}
