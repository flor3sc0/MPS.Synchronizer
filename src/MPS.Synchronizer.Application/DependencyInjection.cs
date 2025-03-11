using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPS.Synchronizer.Application.ExternalApi.Handlers;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Net;
using MPS.Synchronizer.Application.CommonModels;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace MPS.Synchronizer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureRefitClients(services, configuration);

        return services;
    }

    private static void ConfigureRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        var wbApiOptions = configuration.GetSection(WbOptions.Wb).Get<WbOptions>().Api;

        AddRefitClient<IWbStatisticsApi>(services, wbApiOptions.Statistics);
        AddRefitClient<IWbAdvertsApi>(services, wbApiOptions.Adverts);
    }

    private static void AddRefitClient<TApi>(IServiceCollection services, string apiUrl) where TApi : class
    {
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(x => x.StatusCode == HttpStatusCode.TooManyRequests)
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(5, _ => TimeSpan.FromMinutes(1));

        services.AddRefitClient<TApi>(_ => new RefitSettings(new SystemTextJsonContentSerializer(GetDefaultJsonSerializerOptions())))
            .AddPolicyHandler(retryPolicy)
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiUrl))
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

    private class DateOnlyConverterParse : JsonConverter<DateOnly>
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