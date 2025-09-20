using Confluent.Kafka;
using Kafka.Configs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Kafka.Implementations
{
    public class KafkaConsumer<TMessage> : BackgroundService
    {
        private readonly string _topic;
        private readonly IConsumer<string, TMessage> _consumer;

        public KafkaConsumer(IOptions<KafkaSettings> options)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = options.Value.BootstrapServers,
                GroupId = options.Value.BootstrapServers
            };

            _topic = options.Value.Topic;

            _consumer = new ConsumerBuilder<string, TMessage>(config)
                .Build();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
