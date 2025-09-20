using NotificationService.Domain;

namespace NotificationService.Patterns
{
    public static class NotificationPatterns
    {
        public static string ConsoleNotificationPattern(MessageSendedDto dto) => $"{dto.MessageId}{dto.ChatId}{dto.SendTime}";
    }
}
