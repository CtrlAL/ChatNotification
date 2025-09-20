using ChatService.Domain;
using Kafka.Interfaces;

namespace ChatService.Services
{
    public class MessageHandler : IMessageHandler<Message>
    {
        public Task HandleAsync(Message message, CancellationToken cancellationToken)
        {
            Console.WriteLine(message.Conent);

            return Task.CompletedTask;
        }
    }
}
