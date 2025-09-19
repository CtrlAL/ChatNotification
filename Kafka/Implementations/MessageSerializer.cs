using Confluent.Kafka;
using System.Text.Json;

namespace Kafka.Implementations
{
    public class MessageSerializer<TMessage> : ISerializer<TMessage>
    {
        public byte[] Serialize(TMessage data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data);
        }
    }
}
