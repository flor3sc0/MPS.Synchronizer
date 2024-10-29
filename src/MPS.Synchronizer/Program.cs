using Coravel;
using MPS.Synchronizer.Application;
using MPS.Synchronizer.Extensions;


var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScheduler();
builder.Services.AddApplicationDependency(builder.Configuration);
builder.Services.AddPersistenceDependency(builder.Configuration);
builder.Services.AddAppLogging(builder.Configuration);
builder.Services.AddAppOptions(builder.Configuration);

var host = builder.Build();
host.Services.ConfigureScheduler();

await host.MigrateDatabase();
await host.RunAsync();