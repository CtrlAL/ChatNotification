using Kafka.Configs;
using Kafka.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Implementations
{
    public static class Extensions
    {
        public static void AddProducer<TMessage>(this IServiceCollection services, IConfigurationSection section)
        {
            services.Configure<KafkaSettings>(section);
            services.AddScoped<IMessageProducer<TMessage>, MessageProducer<TMessage>>();
        }

        public static void AddConsumer<TMessage, THandler>(this IServiceCollection services, IConfigurationSection section)
            where THandler : class, IMessageHandler<TMessage>
        {
            services.Configure<KafkaSettings>(section);
            services.AddHostedService<KafkaConsumer<TMessage>>();
            services.AddSingleton<IMessageHandler<TMessage>, THandler>();
        }
    }
}
