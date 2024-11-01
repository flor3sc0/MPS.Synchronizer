using Microsoft.EntityFrameworkCore;
using MPS.Synchronizer.Persistence;

namespace MPS.Synchronizer.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistenceDependency(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();

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