using Coravel.Invocable;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using Refit;

namespace MPS.Synchronizer.Application.SynchronizationJobs;

public class WbPingJob(IWbStatisticsApi wbStatisticsApi) : IInvocable
{
    public async Task Invoke()
    {
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
            var pingResponse = await wbPingApi.PingAsync();

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