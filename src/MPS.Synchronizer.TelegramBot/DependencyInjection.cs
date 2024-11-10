using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPS.Synchronizer.TelegramBot.CommonModels;
using Telegram.Bot;

namespace MPS.Synchronizer.TelegramBot;

public static class DependencyInjection
{
    public static IServiceCollection AddTelegramBotDependency(this IServiceCollection services, IConfiguration configuration)
    {
        var telegramBotOptions = configuration.GetSection(TelegramBotOptions.TelegramBot).Get<TelegramBotOptions>();
        if (telegramBotOptions is not { IsEnable: true })
            return services;

        services.AddHttpClient("telegram")
            .AddTypedClient<ITelegramBotClient>(client => new TelegramBotClient(telegramBotOptions.Token, client));

        return services;
    }
}