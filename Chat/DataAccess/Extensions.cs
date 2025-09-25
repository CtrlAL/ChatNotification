using ChatService.Common.Configs;
using ChatService.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatService.DataAccess.Extensions
{
    public static class Extensions
    {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));

            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration.GetConnectionString("MongoConnection");
                return new MongoClient(connectionString);
            });

            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<IOptions<MongoDbSettings>>();
                var databaseName = options.Value.DataAccessName;
                return client.GetDatabase(databaseName);
            });
        }
    }
}
