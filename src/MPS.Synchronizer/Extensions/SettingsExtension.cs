using MPS.Synchronizer.Application.Settings;

namespace MPS.Synchronizer.Extensions;

public static class SettingsExtension
{
    public static IServiceCollection AddAppOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<WbOptions>()
            .Bind(configuration.GetSection(WbOptions.Wb))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}