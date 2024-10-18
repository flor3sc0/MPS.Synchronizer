using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPS.Synchronizer.Application.ExternalApi.Handlers;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using Refit;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using MPS.Synchronizer.Application.SynchronizationJobs.Statistics;
using MPS.Synchronizer.Application.SynchronizationJobs;
using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace MPS.Synchronizer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureServices(services);
        ConfigureRefitClient(services, configuration);

        return services;
    }


    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<WbPingJob>();

        services.AddTransient<StatisticsIncomesSyncJob>();
        services.AddTransient<StatisticsStocksSyncJob>();
        services.AddTransient<StatisticsOrdersSyncJob>();
        services.AddTransient<StatisticsSalesSyncJob>();
        services.AddTransient<StatisticsRealizationReportSyncJob>();
    }

    private static void ConfigureRefitClient(IServiceCollection services, IConfiguration configuration)
    {
        //var retryPolicy = HttpPolicyExtensions
        //    .HandleTransientHttpError()
        //    .Or<TimeoutRejectedException>()
        //    .RetryAsync(5);

        services.AddRefitClient<IWbStatisticsApi>(_ => new RefitSettings(new SystemTextJsonContentSerializer(GetDefaultJsonSerializerOptions())))
            //.AddPolicyHandler(retryPolicy)
            .ConfigureHttpClient(
                c =>
                {
                    c.BaseAddress = new Uri(configuration.GetSection("WbApi:Statistics").Value!);
                    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.GetSection("WbApi:Token").Value);
                })
            .ConfigurePrimaryHttpMessageHandler(
                () =>
                {
                    var httpClientHandler = new RefitHttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                    };
                    return new RefitHttpLoggingHandler(httpClientHandler);
                });
    }

    public class DateOnlyConverterParse : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateOnly));
            return DateOnly.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    /// <summary>
    /// Creates new <see cref="JsonSerializerOptions"/> and fills it with default parameters
    /// </summary>
    /// <returns>Creates new <see cref="JsonSerializerOptions"/> and fills it with default parameters</returns>
    public static JsonSerializerOptions GetDefaultJsonSerializerOptions()
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            // Default to case insensitive property name matching as that's likely the behavior most users expect
            PropertyNameCaseInsensitive = true,
            //PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        //jsonSerializerOptions.Converters.Add(new ObjectToInferredTypesConverter());
        //jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        jsonSerializerOptions.Converters.Add(new DateOnlyConverterParse());

        // Включение сериализации всех кодировок языка без экранирования.
        //jsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
        //jsonSerializerOptions.WriteIndented = true;

        return jsonSerializerOptions;
    }
}