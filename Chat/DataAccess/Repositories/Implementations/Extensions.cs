using ChatService.Common.Constants;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;
using ChatService.Domain.Filters;
using ChatService.Domain.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ChatService.DataAccess.Repositories.Implementations
{
    public static class Extensions
    {
        public static void AddRepositrories(this IServiceCollection services)
        {
            services.AddMongoRepository<Chat, UserResourseFilter, IChatRepository, ChatRepostitory>(
                CollcetionNames.Chats
            );

            services.AddMongoRepository<ChatMessage, UserResourseFilter, IChatMessageRepository, ChatMessageRepository>(
                CollcetionNames.ChatMessages
            );
        }

        public static void AddMongoRepository<TModel, TFilter, TInterface, TImplementation>(this IServiceCollection services, 
            string collectionName, 
            Expression<Func<TModel, object>> field = null)
            where TModel : class, IMongoModel
            where TInterface : class, IMongoRepository<TModel, TFilter>
            where TImplementation : class, TInterface
        {
            services.AddCollection<TModel>(collectionName);
            services.AddScoped<TInterface, TImplementation>();
        }

        private static void AddCollection<TModel>(this IServiceCollection services, string collectionName)
            where TModel : class
        {
            services.AddScoped(sp =>
            {
                var database = sp.GetRequiredService<IMongoDatabase>();

                return database.GetCollection<TModel>(collectionName);
            });
        }

        public static async Task AddIndex<TModel>(this IMongoDatabase database, string collectionName, Expression<Func<TModel, object>> field)
        {
            var chatCollection = database.GetCollection<TModel>(collectionName);
            var indexKeys = Builders<TModel>.IndexKeys.Ascending(field);
            var indexModel = new CreateIndexModel<TModel>(indexKeys);

            await chatCollection.Indexes.CreateOneAsync(indexModel);
        }

        public static async Task InitializeMongoIndexesAsync(this IServiceProvider serviceProvider)
        {
            var database = serviceProvider.GetRequiredService<IMongoDatabase>();

            await database.AddIndex<Chat>(
                CollcetionNames.Chats,
                x => x.UserId
            );

            await database.AddIndex<ChatMessage>(
                CollcetionNames.ChatMessages,
                x => x.UserId
            );
        }
    }
}
