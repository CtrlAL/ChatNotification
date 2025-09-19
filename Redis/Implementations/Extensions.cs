using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.Interfaces;
using StackExchange.Redis;

namespace Redis.Implementations
{
    public static class Extensions
    {
        public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = configuration.GetConnectionString("RedisConnection");
            //    options.InstanceName = "MyTestAppName";
            //});

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });


            services.AddScoped<IRedisCache, RedisCache>();
            services.AddScoped<IRedisPubSub, RedisPubSub>();
        }
    }
}
