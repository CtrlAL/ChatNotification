namespace Kafka.Interfaces
{
    public interface IMessageProducer<TMessage> : IDisposable
    {
        Task ProduceAsync(TMessage message, CancellationToken cancellationToken);
    }
}
