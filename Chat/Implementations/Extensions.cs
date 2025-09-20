using ChatService.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatService.Implementations
{
    public static class Extensions
    {
        public static void AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));

            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration.GetConnectionString("MongoConnection");
                return new MongoClient(connectionString);
            });

            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<IOptions<MongoDbSettings>>();
                var databaseName = options.Value.DatabaseName;
                return client.GetDatabase(databaseName);
            });

            services.AddScoped<MongoDbService>();
        }
    }
}
