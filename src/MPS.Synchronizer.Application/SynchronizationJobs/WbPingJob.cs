using Coravel.Invocable;
using MPS.Synchronizer.Application.CommonModels;
using MPS.Synchronizer.Application.Extensions;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using Refit;
using Serilog;

namespace MPS.Synchronizer.Application.SynchronizationJobs;

public class WbPingJob(IWbStatisticsApi wbStatisticsApi, LegalEntityOptions legalEntityOptions) : IInvocable
{
    public async Task Invoke()
    {
        CheckJwtTtl();

        Log.Information($"Invoke {nameof(WbPingJob)} for '{legalEntityOptions.Name}'");

        await PingAsync(wbStatisticsApi);
    }

    private void CheckJwtTtl()
    {
        var jwtToken = legalEntityOptions.Token.ParseAsJwt();

        var timeToLive = jwtToken.Exp - DateTime.Now;
        var daysToLive = timeToLive.TotalDays;
        if (daysToLive < 10)
        {
            Log.Warning("{daysToLive} дней до истечения токена для {LegalEntity}", daysToLive, legalEntityOptions.Name);
        }
    }

    /// <summary>
    /// заменить bool на расширенный результат залогировать и что-то предпринять
    /// </summary>
    /// <returns></returns>
    private async Task<bool> PingAsync(IWbBaseApi wbPingApi)
    {
        try
        {
            var pingResponse = await wbPingApi.PingAsync(legalEntityOptions.Token);

            return pingResponse?.Status == "OK";
        }
        catch (ValidationApiException apiException)
        {
            // залогировать
            Console.WriteLine(apiException);
            throw;
            //return false;
        }
    }
}