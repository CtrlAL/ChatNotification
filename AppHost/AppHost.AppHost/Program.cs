var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ChatService>("Chat");
builder.AddProject<Projects.TelegramService>("TelegramService");
builder.AddProject<Projects.NotificationService>("Notification");
builder.AddProject<Projects.RegistrationService>("registrationservice");

var mongo = builder.AddMongoDB("mongodb")
                   .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "admin")
                   .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "123");

var redis = builder.AddRedis("redis");

var kafka = builder.AddContainer("kafka", "apache/kafka:latest")
                   .WithEndpoint(port: 9092, targetPort: 9092)
                   .WithEndpoint(port: 9093, targetPort: 9093);

var keycloak = builder.AddContainer("keycloak", "quay.io/keycloak/keycloak:24.0")
                      .WithHttpEndpoint(port: 8090, targetPort: 8080)
                      .WithEnvironment("KEYCLOAK_ADMIN", "admin")
                      .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "admin")
                      .WithArgs("start-dev");

builder.Build().Run();
