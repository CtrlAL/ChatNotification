using ChatService.Common.Constants;
using ChatService.DataAccess.Repositories.Interfaces;
using ChatService.Domain;
using ChatService.Domain.Filters;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ChatService.DataAccess.Repositories.Implementations
{
    public static class Extensions
    {
        public static void AddRepositrories(this IServiceCollection services)
        {
            services.AddMongoRepository<Chat, UserResourseFilter, IChatRepository, ChatRepostitory>(
                CollcetionNames.Chats,
                x => x.UserId
            );

            services.AddMongoRepository<ChatMessage, UserResourseFilter, IChatMessageRepository, ChatMessageRepository>(
                CollcetionNames.ChatMessages,
                x => x.UserId
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

        private static void AddCollection<TModel>(this IServiceCollection services, string collectionName, Expression<Func<TModel, object>> field = null)
            where TModel : class
        {
            services.AddScoped(sp =>
            {
                var database = sp.GetRequiredService<IMongoDatabase>();

                if (field != null)
                {
                    database.AddIndex<TModel>(collectionName, field);
                }

                return database.GetCollection<TModel>(collectionName);
            });
        }

        private static async Task AddIndex<TModel>(this IMongoDatabase database, string collectionName, Expression<Func<TModel, object>> field)
        {
            var chatCollection = database.GetCollection<TModel>(collectionName);
            var indexKeys = Builders<TModel>.IndexKeys.Ascending(field);
            var indexModel = new CreateIndexModel<TModel>(indexKeys);

            await chatCollection.Indexes.CreateOneAsync(indexModel);
        }
    }
}
