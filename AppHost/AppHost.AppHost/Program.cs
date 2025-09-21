var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ChatService>("Chat");
builder.AddProject<Projects.NotificationService>("Notification");

builder.Build().Run();
