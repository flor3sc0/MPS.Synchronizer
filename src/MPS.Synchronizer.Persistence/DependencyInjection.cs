using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MPS.Synchronizer.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceDependency(this IServiceCollection services,
        IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options.EnableDetailedErrors();
#if DEBUG
            options.UseLoggerFactory(serviceProvider.GetService<ILoggerFactory>());
#endif
            options.UseNpgsql(
                configuration.GetSection("ConnectionStrings:PostgresConnection:connectionString").Value);
        });

        return services;
    }

    public static async Task MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            await context.MigrateAsync();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
            logger.LogError(ex, "An error occurred while migrating or initializing the database");
            throw;
        }
    }
}