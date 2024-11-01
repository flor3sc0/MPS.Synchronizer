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

public class StatisticsSalesSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext, LegalEntityOptions options) : IInvocable
{
    public async Task Invoke()
    {
        Log.Information($"Invoke {GetType().Name} for '{options.Name}'");

        var alreadyInit = await appDbContext.Set<StatisticsSale>().AnyAsync(x => x.LegalEntity == options.Name);
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

        await CreateOrUpdateItems(items, []);
    }

    private async Task InitSync()
    {
        var date = DateTime.Parse("2024-01-01").Date;

        var allItems = await GetItems(date, 0);
        var saleIds = allItems.Select(x => x.SaleId).ToList();
        var lastDate = allItems.Min(x => x.LastChangeDate).Date;
        await SaveChanges(allItems);
        await Task.Delay(TimeSpan.FromMinutes(0.5));

        var totalCount = allItems.Count;
        allItems.Clear();

        while (date <= lastDate)
        {
            List<StatisticsSale> items;
            try
            {
                items = await GetItems(date, 1);
            }
            catch (Exception e)
            {
                Log.Warning($"Большое количество запросов при инициализации {GetType().Name} для '{options.Name}'");
                await Task.Delay(TimeSpan.FromMinutes(1));
                items = await GetItems(date, 1);
            }

            var delayTask = Task.Delay(TimeSpan.FromMinutes(1));

            await CreateOrUpdateItems(items, saleIds);
            await delayTask;
            date = date.AddDays(1);
            totalCount += items.Count;
        }

        Log.Information($"Invoked [INIT] {GetType().Name} for '{options.Name}' with {totalCount} items\n");
    }

    private async Task CreateOrUpdateItems(List<StatisticsSale> items, List<string> saleIdsToSkip)
    {
        var newSaleIds = items.Select(x => x.SaleId);
        var existed = await appDbContext.Set<StatisticsSale>()
            .Select(x => new
            {
                Id = x.Id,
                SaleId = x.SaleId,
            })
            .Where(x => newSaleIds.Contains(x.SaleId))
            .ToDictionaryAsync(x => x.SaleId, x => x.Id);

        var toCreate = new List<StatisticsSale>();
        foreach (var item in items)
        {
            if (saleIdsToSkip.Contains(item.Srid))
                continue;

            if (existed.TryGetValue(item.SaleId, out var id))
            {
                item.Id = id;

                appDbContext.Set<StatisticsSale>().Entry(item).State = EntityState.Modified;
                appDbContext.Set<StatisticsSale>().Entry(item).Property(p => p.LegalEntity).IsModified = false;
            }
            else
            {
                toCreate.Add(item);
            }
        }

        await SaveChanges(toCreate);
        var created = toCreate.Count;
        var updated = items.Count - toCreate.Count;
        Log.Information($"Invoked {GetType().Name} for '{options.Name}': {created} created; {updated} updated;\n");
    }

    private async Task<List<StatisticsSale>> GetItems(DateTime dateFrom, int flag = 0)
    {
        var request = new StatisticsSalesRequest(dateFrom, flag);
        return await apiService.GetSalesAsync(options.Token, request);
    }

    private async Task SaveChanges(List<StatisticsSale> items)
    {
        items.EnrichByLegalEntity(options.Name);
        await appDbContext.Set<StatisticsSale>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }
}