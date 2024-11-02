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

/// <summary>
/// Историчность остататков на складе на каждый день
/// </summary>
public class StatisticsStocksSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext, LegalEntityOptions options) : IInvocable
{
    public async Task Invoke()
    {
        Log.Information($"Invoke {GetType().Name} for '{options.Name}'");

        var alreadyInit = await appDbContext.Set<StatisticsStock>().AnyAsync(x => x.LegalEntity == options.Name);

        if (alreadyInit)
        {
            await BaseSync();
            return;
        }

        await InitSync();
    }

    private async Task BaseSync()
    {
        var dateFrom = DateTime.Now.AddDays(-1);
        var items = await GetItems(dateFrom);

        var filtered = items.Where(x => x.LastChangeDate.Date == dateFrom.Date).ToList();
        await SaveChanges(filtered);
    }

    private async Task InitSync()
    {
        var items = await GetItems(DateTime.Parse("2020-01-01"));

        await SaveChanges(items);
    }

    private async Task<List<StatisticsStock>> GetItems(DateTime dateFrom)
    {
        var request = new StatisticsStocksRequest(dateFrom);
        return await apiService.GetStocksAsync(options.Token, request);
    }

    private async Task SaveChanges(List<StatisticsStock> items)
    {
        items.EnrichByLegalEntity(options.Name);
        await appDbContext.Set<StatisticsStock>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
        Log.Information($"Invoked {GetType().Name} for '{options.Name}' with {items.Count} items\n");
    }
}