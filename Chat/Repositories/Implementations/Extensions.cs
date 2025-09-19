using Chat.Repositories.Interfaces;

namespace Chat.Repositories.Implementations
{
    public static class Extensions
    {
        public static void AddRepositrories(this IServiceCollection services)
        {
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        }
    }
}
