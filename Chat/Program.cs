using ChatService.DataAccess.Extensions;
using ChatService.DataAccess.Repositories.Implementations;
using ChatService.Domain.Dto;
using Kafka.Implementations;
using KeycloakAuth;
using Microsoft.OpenApi.Models;
using Redis.Implementations;
using ChatService.Services.Extensions;
using ChatService.API.Hubs;
using ChatService.Policy;

namespace ChatService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Введите 'Bearer' [пробел] и ваш JWT-токен:\r\n\r\n" +
                                  "Пример: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            builder.Services.AddRepositrories();
            builder.Services.AddRedis(builder.Configuration);
            builder.Services.AddMongoDB(builder.Configuration);
            builder.Services.AddSignalR();
            builder.Services.AddServices();

            builder.Services.AddProducer<MessageSendedDto>(builder.Configuration.GetSection("KafkaSettings"));
            builder.Services.AddKeyCloak(builder.Configuration.GetSection("KeyCloak"));
            builder.Services.AddPolicies();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                await scope.ServiceProvider.InitializeMongoIndexesAsync();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapHub<ChatHub>("api/chat-hub");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
