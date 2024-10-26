using Coravel.Invocable;
using MPS.Synchronizer.Application.Extensions;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.ExternalApi.Models.Statistics;
using MPS.Synchronizer.Application.Settings;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

public class StatisticsOrdersSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext, LegalEntityOptions legalEntityOptions) : IInvocable
{
    public async Task Invoke()
    {
        var request = new StatisticsOrdersRequest(DateTime.Parse("2020-01-01"), 0);
        var items = await apiService.GetOrdersAsync(legalEntityOptions.Token, request);

        items.EnrichByLegalEntity(legalEntityOptions.Name);
        await appDbContext.Set<StatisticsOrder>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }
}