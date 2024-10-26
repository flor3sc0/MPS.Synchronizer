using Coravel.Invocable;
using MPS.Synchronizer.Application.Extensions;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.ExternalApi.Models.Statistics;
using MPS.Synchronizer.Application.Settings;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

public class StatisticsRealizationReportSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext, LegalEntityOptions legalEntityOptions) : IInvocable
{
    public async Task Invoke()
    {
        //var request = new StatisticsRealizationReportRequest(DateTime.Parse("2024-01-01"), DateTime.Parse("2024-10-17"), 0);
        var request = new StatisticsRealizationReportRequest(DateTime.Parse("2024-10-13"), DateTime.Parse("2024-10-16"));

        var items = await GetItems(request);

        items.EnrichByLegalEntity(legalEntityOptions.Name);
        await appDbContext.Set<StatisticsRealizationReport>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }

    private async Task<List<StatisticsRealizationReport>> GetItems(StatisticsRealizationReportRequest request, long rrdId = 0)
    {
        request.Rrdid = rrdId;
        request.Limit = 100_000;
        var items = await apiService.GetReportDetailByPeriodAsync(legalEntityOptions.Token, request);
        if (items.Count < request.Limit)
        {
            return items;
        }

        var lastRrdId = items.Last().RrdId;
        var nextItems = await GetItems(request, lastRrdId);

        items.AddRange(nextItems);
        return items;
    }
}