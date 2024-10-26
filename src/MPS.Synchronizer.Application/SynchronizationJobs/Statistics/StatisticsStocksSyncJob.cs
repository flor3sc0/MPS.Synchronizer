using Coravel.Invocable;
using MPS.Synchronizer.Application.Extensions;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.ExternalApi.Models.Statistics;
using MPS.Synchronizer.Application.Settings;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

public class StatisticsStocksSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext, LegalEntityOptions legalEntityOptions) : IInvocable
{
    public async Task Invoke()
    {
        var request = new StatisticsStocksRequest(DateTime.Parse("2024-10-13"));
        var items = await apiService.GetStocksAsync(legalEntityOptions.Token, request);

        items.EnrichByLegalEntity(legalEntityOptions.Name);
        await appDbContext.Set<StatisticsStock>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }
}