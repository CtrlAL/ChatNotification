using KeycloakAuth;
using RegistrationService.Constants;
using RegistrationService.Services;

namespace RegistrationService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;
        var keycloak = config.GetSection("KeyCloak");

        builder.AddServiceDefaults();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddKeyCloakClient(keycloak, KeycloakConstants.HttpClientName);
        builder.Services.AddScoped<IKeyCloakManager, KeycloakManager>();

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
