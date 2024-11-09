using Serilog;

namespace MPS.Synchronizer.Extensions;

public static class LoggingStartupExtension
{
    public static IServiceCollection AddAppLogging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true));

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:dd.MM.yyyy HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        Log.Logger = logger;

        var loggerFactory = new LoggerFactory();
        loggerFactory.AddSerilog(logger);
        services.AddSingleton<ILoggerFactory>(loggerFactory);

        return services;
    }
}