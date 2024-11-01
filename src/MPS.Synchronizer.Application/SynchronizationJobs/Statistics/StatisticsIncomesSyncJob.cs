using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using MPS.Synchronizer.Application.CommonModels;
using MPS.Synchronizer.Application.Extensions;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.ExternalApi.Models.Statistics;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence;
using Serilog;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

public class StatisticsIncomesSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext, LegalEntityOptions legalEntityOptions) : IInvocable
{
    public async Task Invoke()
    {
        Log.Information($"Invoke {GetType().Name} for '{legalEntityOptions.Name}'");

        var request = new StatisticsIncomesRequest(DateTime.Parse("2020-01-01"));
        var items = await apiService.GetIncomesAsync(legalEntityOptions.Token, request);

        await appDbContext.Set<StatisticsIncome>()
            .Where(x => x.LegalEntity == legalEntityOptions.Name)
            .ExecuteDeleteAsync();

        items.EnrichByLegalEntity(legalEntityOptions.Name);
        await appDbContext.Set<StatisticsIncome>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();

        Log.Information($"Invoked {GetType().Name} for '{legalEntityOptions.Name}' with {items.Count} items\n");
    }
}