using ChatService.Services.Interfaces;

namespace ChatService.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
        }
    }
}
