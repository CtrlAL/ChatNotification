using Kafka.Implementations;
using TelegramService.Domain;
using TelegramService.Extensions;
using TelegramService.NotificationHandler;

namespace TelegramService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;

        builder.AddServiceDefaults();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddConsumer<MessageSendedDto, TelegramNotificationHandler>(config.GetSection("KafkaSettings"));
        builder.Services.AddTelegramClient(config.GetSection("Telegram"));

        var app = builder.Build();

        app.MapDefaultEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
