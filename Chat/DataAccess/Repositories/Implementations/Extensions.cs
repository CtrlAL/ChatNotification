using ChatService.Common.Constants;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;
using MongoDB.Driver;

namespace ChatService.DataAccess.Repositories.Implementations
{
    public static class Extensions
    {
        public static void AddRepositrories(this IServiceCollection services)
        {
            services.AddMongoRepository<Chat, object, IChatRepository, ChatRepostitory>(
                CollcetionNames.Chats
            );

            services.AddMongoRepository<ChatMessage, object, IChatMessageRepository, ChatMessageRepository>(
                CollcetionNames.Chats
            );
        }

        private static void AddCollection<TModel>(this IServiceCollection services, string collectionName)
            where TModel : class
        {
            services.AddScoped(sp =>
            {
                var database = sp.GetRequiredService<IMongoDataAccess>();
                return database.GetCollection<TModel>(collectionName);
            });
        }

        public static void AddMongoRepository<TModel, TFilter, TInterface, TImplementation>(
            this IServiceCollection services,
            string collectionName)
            where TModel : class, IMongoModel
            where TInterface : class, IMongoRepository<TModel, TFilter>
            where TImplementation : class, TInterface
        {
            services.AddCollection<TModel>(collectionName);
            services.AddScoped<TInterface, TImplementation>();
        }
    }
}
