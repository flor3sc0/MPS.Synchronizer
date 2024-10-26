using Coravel.Invocable;
using MPS.Synchronizer.Application.Extensions;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.ExternalApi.Models.Statistics;
using MPS.Synchronizer.Application.Settings;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

public class StatisticsSalesSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext, LegalEntityOptions legalEntityOptions) : IInvocable
{
    public async Task Invoke()
    {
        var request = new StatisticsSalesRequest(DateTime.Parse("2023-01-01"), 0);
        var items = await apiService.GetSalesAsync(legalEntityOptions.Token, request);

        items.EnrichByLegalEntity(legalEntityOptions.Name);
        await appDbContext.Set<StatisticsSale>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }
}