using ChatService.Domain.Dto;
using Kafka.Interfaces;

namespace ChatService.Services
{
    public class MessageHandler : IMessageHandler<MessageSendedDto>
    {
        public Task HandleAsync(MessageSendedDto message, CancellationToken cancellationToken)
        {
            Console.WriteLine(message.Conent);

            return Task.CompletedTask;
        }
    }
}
