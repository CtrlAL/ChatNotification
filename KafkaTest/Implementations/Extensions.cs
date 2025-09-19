using KafkaTest.Configs;
using KafkaTest.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StackExchange.Redis;

namespace KafkaTest.Implementations
{
    public static class Extensions
    {
        public static void AddProducer<TMessage>(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaSettings>(configuration.GetSection("KafkaSettings"));
            services.AddScoped<IMessageProducer<TMessage>, MessageProducer<TMessage>>();
        }

        public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "MyTestAppName";
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });


            services.AddScoped<IRedisCache, RedisCache>();
            services.AddScoped<IRedisPubSub, RedisPubSub>();
        }

        public static void AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration.GetConnectionString("MongoConnection");
                return new MongoClient(connectionString);
            });

            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var databaseName = configuration.GetConnectionString("MongoConnection");
                return client.GetDatabase(databaseName);
            });
        }
    }
}
