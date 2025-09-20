using ChatService.Domain;
using ChatService.Implementations;
using ChatService.Repositories.Implementations;
using ChatService.Services;
using Kafka.Implementations;
using Redis.Implementations;

namespace ChatService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddRepositrories();
            builder.Services.AddRedis(builder.Configuration);
            builder.Services.AddMongo(builder.Configuration);

            builder.Services.AddProducer<Message>(builder.Configuration.GetSection("KafkaSettings"));
            builder.Services.AddConsumer<Message, MessageHandler>(builder.Configuration.GetSection("KafkaSettings"));

            var app = builder.Build();

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
}
