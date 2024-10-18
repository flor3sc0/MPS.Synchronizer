using Coravel.Invocable;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.ExternalApi.Models.Statistics;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

public class StatisticsRealizationReportSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext) : IInvocable
{
    public async Task Invoke()
    {
        //var request = new StatisticsRealizationReportRequest(DateTime.Parse("2024-01-01"), DateTime.Parse("2024-10-17"), 0);
        var request = new StatisticsRealizationReportRequest(DateTime.Parse("2024-10-13"), DateTime.Parse("2024-10-16"), 0);
        //var items = await apiService.GetReportDetailByPeriodAsync(request);

        var items = await GetItems(request);

        await appDbContext.Set<StatisticsRealizationReport>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }

    private async Task<List<StatisticsRealizationReport>> GetItems(StatisticsRealizationReportRequest request, long rrdid = 0)
    {
        request.Rrdid = rrdid;
        request.Limit = 100_000;
        var items = await apiService.GetReportDetailByPeriodAsync(request);
        if (items.Count < request.Limit)
        {
            return items;
        }

        var last_rrdid = items.Last().RrdId;
        var items2 = await GetItems(request, last_rrdid);

        items.AddRange(items2);
        return items;
    }
}