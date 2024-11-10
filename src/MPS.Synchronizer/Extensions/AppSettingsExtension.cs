using MPS.Synchronizer.Application.CommonModels;
using MPS.Synchronizer.TelegramBot.CommonModels;

namespace MPS.Synchronizer.Extensions;

public static class AppSettingsExtension
{
    public static IServiceCollection AddAppOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<WbOptions>()
            .Bind(configuration.GetSection(WbOptions.Wb))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<TelegramBotOptions>()
            .Bind(configuration.GetSection(TelegramBotOptions.TelegramBot));

        return services;
    }
}