var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ChatService>("Chat");
builder.AddProject<Projects.NotificationService>("Notification");

builder.AddProject<Projects.TelegramService>("telegramservice");

builder.Build().Run();
