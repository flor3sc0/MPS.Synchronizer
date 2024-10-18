using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.ExternalApi.Models.Statistics;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

public class StatisticsIncomesSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext) : IInvocable
{
    public async Task Invoke()
    {
        var request = new StatisticsIncomesRequest(DateTime.Parse("2024-10-16"));
        var items = await apiService.GetIncomesAsync(request);

        await appDbContext.Set<StatisticsIncome>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }
}