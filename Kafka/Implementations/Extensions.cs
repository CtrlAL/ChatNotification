using Kafka.Configs;
using Kafka.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Implementations
{
    public static class Extensions
    {
        public static void AddProducer<TMessage>(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaSettings>(configuration.GetSection("KafkaSettings"));
            services.AddScoped<IMessageProducer<TMessage>, MessageProducer<TMessage>>();
        }
    }
}
