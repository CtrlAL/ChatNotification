using Kafka.Interfaces;
using NotificationService.Domain;
using NotificationService.Patterns;

namespace NotificationService.MessageHandler
{
    public class ConsoleNotificationHandler : IMessageHandler<MessageSendedDto>
    {
        public Task HandleAsync(MessageSendedDto message, CancellationToken cancellationToken)
        {
            var notificationMessage = NotificationPatterns.ConsoleNotificationPattern(message);

            Console.WriteLine(notificationMessage);

            return Task.CompletedTask;
        }
    }
}
