using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using MPS.Synchronizer.Application.CommonModels;
using MPS.Synchronizer.Application.Extensions;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence;
using Refit;
using Serilog;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using MPS.Synchronizer.TelegramBot.CommonModels;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace MPS.Synchronizer.Application.SynchronizationJobs;

public class WbPingJob(
    AppDbContext appDbContext,
    IWbStatisticsApi wbStatisticsApi,
    IServiceProvider serviceProvider,
    IOptions<TelegramBotOptions> tgOptions,
    LegalEntityOptions options
    ) : IInvocable
{
    public async Task Invoke()
    {
        Log.Information($"Invoke {nameof(WbPingJob)} for '{options.Name}'");

        var jwtTtl = CheckJwtTtl();
        var statisticsApiPing = await ApiPingAsync(wbStatisticsApi);
        var dbPing = await DbPingAsync();

        var result = new Dictionary<string, string>
        {
            { "LegalEntity", options.Name },
            { "JwtTtl", jwtTtl },
            { "StatisticsApi", statisticsApiPing },
            { "Db", dbPing },
        };

        var message = string.Join(Environment.NewLine, result.Select(pair => $"<b>{pair.Key}</b>: {pair.Value}"));
        await SendTelegramMessage(message);
    }

    private string CheckJwtTtl()
    {
        try
        {
            var jwtToken = options.Token.ParseAsJwt();
            var timeToLive = jwtToken.Exp - DateTime.Now;
            var daysToLive = Math.Round(timeToLive.TotalDays, 1);
            return $"{daysToLive} дней до истечения токена";
        }
        catch (Exception e)
        {
            return "Error parsing Jwt";
        }
    }

    private async Task<string> ApiPingAsync(IWbBaseApi wbPingApi)
    {
        try
        {
            var pingResponse = await wbPingApi.PingAsync(options.Token);

            return pingResponse?.Status;
        }
        catch (ValidationApiException apiException)
        {
            Log.Warning("[DbPing - {LegalEntity}]: {Exception}", options.Name, apiException);
            return $"Error - StatusCode = {apiException.StatusCode}";
        }
    }

    private async Task<string> DbPingAsync()
    {
        try
        {
            await appDbContext.Set<StatisticsIncome>().AnyAsync();
            return "OK";
        }
        catch (Exception exception)
        {
            Log.Warning("[DbPing - {LegalEntity}]: {Exception}", options.Name, exception);
            return $"Error - {exception.Message}";
        }
    }

    private async Task SendTelegramMessage(string message)
    {
        if (!tgOptions.Value.IsEnable)
        {
            Log.Information("[Ping]: {Message}", message);
            return;
        }

        try
        {
            var telegramBotClient = serviceProvider.GetService<ITelegramBotClient>();
            await telegramBotClient.SendMessage(tgOptions.Value.ChatId, message, ParseMode.Html);
        }
        catch (Exception e)
        {
            Log.Warning("Error send HealthCheck message to Telegram chat {ExceptionMessage}", e.Message);
        }
    }
}