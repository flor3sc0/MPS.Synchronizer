using Coravel.Invocable;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.Settings;
using Refit;
using Serilog;

namespace MPS.Synchronizer.Application.SynchronizationJobs;

public class WbPingJob(IWbStatisticsApi wbStatisticsApi, LegalEntityOptions legalEntityOptions) : IInvocable
{
    public async Task Invoke()
    {
        Log.Information($"Invoke {nameof(WbPingJob)} for '{legalEntityOptions.Name}'");

        await PingAsync(wbStatisticsApi);
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