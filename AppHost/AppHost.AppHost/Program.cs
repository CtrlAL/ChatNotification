using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("mongodb-auth")
                   .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "admin")
                   .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "123");

var redis = builder.AddRedis("redis");

var kafka = builder.AddKafka("kafka");

var keycloak = builder.AddKeycloakContainer("keycloak").AddRealm("dev");

builder.AddProject<Projects.ChatService>("Chat")
       .WithReference(mongo)
       .WithReference(redis)
       .WithReference(kafka)
       .WithReference(keycloak);

builder.AddProject<Projects.TelegramService>("TelegramService")
       .WithReference(redis)
       .WithReference(kafka);

builder.AddProject<Projects.NotificationService>("Notification")
       .WithReference(kafka);

builder.AddProject<Projects.RegistrationService>("RegistrationService")
       .WithReference(mongo)
       .WithReference(keycloak);

builder.Build().Run();