using TelegramService.Domain;

namespace TelegramService.Patterns
{
    public static class NotificationPatterns
    {
        public static string ConsoleNotificationPattern(MessageSendedDto dto) => $"{dto.MessageId}{dto.ChatId}{dto.SendTime}";
    }
}
