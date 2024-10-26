using Coravel.Invocable;
using MPS.Synchronizer.Application.Extensions;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.ExternalApi.Models.Statistics;
using MPS.Synchronizer.Application.Settings;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

public class StatisticsIncomesSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext, LegalEntityOptions legalEntityOptions) : IInvocable
{
    public async Task Invoke()
    {
        var request = new StatisticsIncomesRequest(DateTime.Parse("2024-01-01"));
        var items = await apiService.GetIncomesAsync(legalEntityOptions.Token, request);

        items.EnrichByLegalEntity(legalEntityOptions.Name);
        await appDbContext.Set<StatisticsIncome>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }
}