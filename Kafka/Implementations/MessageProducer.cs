using Confluent.Kafka;
using Kafka.Configs;
using Kafka.Interfaces;
using Microsoft.Extensions.Options;

namespace Kafka.Implementations
{
    public class MessageProducer<TMessage> : IMessageProducer<TMessage>
    {
        private readonly IProducer<string, TMessage> _producer;
        private readonly string _topic;

        public MessageProducer(IOptions<KafkaSettings> options)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = options.Value.BootstrapServers,
                SocketTimeoutMs = 10000,
                MessageTimeoutMs = 10000,
                RequestTimeoutMs = 10000,
            };

            _producer = new ProducerBuilder<string, TMessage>(config)
                .SetValueSerializer(new MessageSerializer<TMessage>())
                .Build();

            _topic = options.Value.Topic;
        }

        public async Task ProduceAsync(TMessage message, CancellationToken cancellationToken)
        {
            await _producer.ProduceAsync(_topic, new Message<string, TMessage>
            {
                Key = "1",
                Value = message
            }, cancellationToken);
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}
