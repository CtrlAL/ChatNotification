using Kafka.Interfaces;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramService.Configs;
using TelegramService.Domain;
using TelegramService.Patterns;

namespace TelegramService.NotificationHandler
{
    public class TelegramNotificationHandler : IMessageHandler<MessageSendedDto>
    {
        private readonly ITelegramBotClient _botClient;
        private readonly TelegramSettigns _settings;

        public TelegramNotificationHandler(ITelegramBotClient botClient, IOptions<TelegramSettigns> options)
        {
            _botClient = botClient;
            _settings = options.Value;
        }

        public async Task HandleAsync(MessageSendedDto message, CancellationToken cancellationToken)
        {
            try
            {
                var text = NotificationPatterns.ConsoleNotificationPattern(message);

                await _botClient.SendMessage(chatId: _settings.ChatId, text: message.SendTime.ToString());
            }
            catch(Exception ex)
            {

            }
        }
    }
}
