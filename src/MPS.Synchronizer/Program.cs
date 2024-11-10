using Coravel;
using MPS.Synchronizer.Application;
using MPS.Synchronizer.Extensions;
using MPS.Synchronizer.TelegramBot;


var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddAppOptions(builder.Configuration);
builder.Services.AddAppLogging(builder.Configuration);
builder.Services.AddScheduler();
builder.Services.AddApplicationDependency(builder.Configuration);
builder.Services.AddPersistenceDependency(builder.Configuration);
builder.Services.AddTelegramBotDependency(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var host = builder.Build();
host.Services.ConfigureScheduler();

await host.MigrateDatabase();
await host.RunAsync();