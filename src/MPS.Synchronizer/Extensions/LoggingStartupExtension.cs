using MPS.Synchronizer.TelegramBot.CommonModels;
using Serilog;
using Serilog.Events;

namespace MPS.Synchronizer.Extensions;

public static class LoggingStartupExtension
{
    public static IServiceCollection AddAppLogging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true));

        var loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:dd.MM.yyyy HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}");

        var telegramBotOptions = configuration.GetSection(TelegramBotOptions.TelegramBot).Get<TelegramBotOptions>();
        if (telegramBotOptions is { IsEnable: true })
        {
            loggerConfiguration.WriteTo.Telegram(
                restrictedToMinimumLevel: LogEventLevel.Error,
                botToken: telegramBotOptions.Token,
                chatId: telegramBotOptions.ChatId.ToString(),
                applicationName: "MPS.Synchronizer");
        }

        var logger = loggerConfiguration.CreateLogger();

        Log.Logger = logger;

        var loggerFactory = new LoggerFactory();
        loggerFactory.AddSerilog(logger);
        services.AddSingleton<ILoggerFactory>(loggerFactory);

        return services;
    }
}