using TelegramService.Domain;

namespace TelegramService.Patterns
{
    public static class NotificationPatterns
    {
        public static string ConsoleNotificationPattern(MessageSendedDto dto)
            => @$"📩 Вам пришло новое сообщение!
    
                🔹 Чат: ID {dto.ChatId}
                🔹 Время: {dto.SendTime:dd.MM.yyyy HH:mm:ss}
                🔹 ID сообщения: {dto.MessageId}";
    }
}
