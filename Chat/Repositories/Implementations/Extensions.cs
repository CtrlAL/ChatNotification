using ChatService.Configs;
using ChatService.Domain;
using ChatService.Repositories.Interfaces;
using MongoDB.Driver;

namespace ChatService.Repositories.Implementations
{
    public static class Extensions
    {
        public static void AddRepositrories(this IServiceCollection services)
        {
            services.AddColection<Chat>(CollcetionNames.Chats);
            services.AddColection<ChatMessage>(CollcetionNames.ChatMessages);

            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IChatRepository, ChatRepostitory>();
        }

        private static void AddColection<TModel>(this IServiceCollection services, string collectionName)
            where TModel : class
        {
            services.AddScoped(sp =>
            {
                var database = sp.GetRequiredService<IMongoDatabase>();
                return database.GetCollection<TModel>(collectionName);
            });
        }
    }
}
