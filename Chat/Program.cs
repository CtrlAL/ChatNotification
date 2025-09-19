using Chat.Domain;
using Chat.Implementations;
using Chat.Repositories.Implementations;

namespace Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddProducer<Message>(builder.Configuration);
            builder.Services.AddRedis(builder.Configuration);
            builder.Services.AddMongo(builder.Configuration);
            builder.Services.AddRepositrories();

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
