using ChatService.Domain.Dto;
using ChatService.Implementations;
using ChatService.Repositories.Implementations;
using Kafka.Implementations;
using KeycloakAuth;
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

            builder.Services.AddProducer<MessageSendedDto>(builder.Configuration.GetSection("KafkaSettings"));
            builder.Services.AddKeyCloak(builder.Configuration.GetSection("KeyCloak"));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
