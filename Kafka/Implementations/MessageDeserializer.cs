using Confluent.Kafka;
using System.Text.Json;

namespace Kafka.Implementations
{
    public class MessageDeserializer<TMessage> : IDeserializer<TMessage>
    {
        public TMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return JsonSerializer.Deserialize<TMessage>(data)!;
        }
    }
}
