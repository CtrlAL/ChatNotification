namespace ChatService.Services.Interfaces
{
    public interface IConnectionService
    {
        Task AddConnectionAsync(string userId, string connectionId);
        Task<int> RemoveConnectionAsync(string connectionId);
    }
}
