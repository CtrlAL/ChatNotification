using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KeycloakAuth
{
    public static class Extensions
    {
        public static void AddKeyCloak(this IServiceCollection services, IConfigurationSection section)
        {
            var keycloak = section.Get<KeyCloakSettings>() 
                ?? throw new InvalidOperationException("KeyCloak settings missing");

            if (string.IsNullOrWhiteSpace(keycloak.Realm))
                throw new InvalidOperationException("KeyCloak:Realm is required");
            if (string.IsNullOrWhiteSpace(keycloak.Audience))
                throw new InvalidOperationException("KeyCloak:Audience is required");

            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = $"http://{keycloak.Host}:{keycloak.Port}/realms/{keycloak.Realm}/";
                options.Audience = keycloak.Audience;
                options.RequireHttpsMetadata = false;
            });

            services.AddAuthorization();
        }

        public static void AddKeyCloakClient(this IServiceCollection services, IConfigurationSection section, string clientName)
        {
            services.AddHttpClient(clientName, (sp, client) =>
            {
                var settings = sp.GetRequiredService<IOptions<KeyCloakSettings>>().Value;
                client.BaseAddress = new Uri($"http://{settings.Host}:{settings.Port}/");
                client.Timeout = TimeSpan.FromSeconds(30);
            });
        }
    }
}
