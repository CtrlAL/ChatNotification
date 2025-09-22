using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramService.Configs;

namespace TelegramService.Extensions
{
    public static class Extensions
    {
        public static void AddTelegramClient(this IServiceCollection services, IConfigurationSection section)
        {
            services.Configure<TelegramSettigns>(section);
            services.AddScoped<ITelegramBotClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<TelegramSettigns>>();
                return new TelegramBotClient(settings.Value.BotToken);
            });
        }
    }
}