using Coravel.Invocable;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.ExternalApi.Models.Statistics;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

public class StatisticsSalesSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext) : IInvocable
{
    public async Task Invoke()
    {
        var request = new StatisticsSalesRequest(DateTime.Parse("2023-01-01"), 0);
        var items = await apiService.GetSalesAsync(request);

        await appDbContext.Set<StatisticsSale>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }
}