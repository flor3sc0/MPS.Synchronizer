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

public class StatisticsOrdersSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext, LegalEntityOptions options) : IInvocable
{
    public async Task Invoke()
    {
        Log.Information($"Invoke {GetType().Name} for '{options.Name}'");

        var alreadyInit = await appDbContext.Set<StatisticsOrder>().AnyAsync(x => x.LegalEntity == options.Name);
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

        await CreateOrUpdateItems(items);
    }

    private async Task InitSync()
    {
        var date = DateTime.Parse("2024-01-01").Date;

        var allItems = await GetItems(date, 0);
        var lastDate = allItems.Min(x => x.LastChangeDate).Date;
        await SaveChanges(allItems);
        await Task.Delay(TimeSpan.FromMinutes(0.5));

        var totalCount = allItems.Count;
        allItems.Clear();

        while (date <= lastDate)
        {
            var items = await GetItems(date, 1);
            var delayTask = Task.Delay(TimeSpan.FromMinutes(1));

            await CreateOrUpdateItems(items);
            await delayTask;
            date = date.AddDays(1);
            totalCount += items.Count;
        }

        Log.Information($"Invoked [INIT] {GetType().Name} for '{options.Name}' with {totalCount} items\n");
    }

    private async Task CreateOrUpdateItems(List<StatisticsOrder> items)
    {
        var newSrids = items.Select(x => x.Srid);
        var existed = await appDbContext.Set<StatisticsOrder>()
            .Select(x => new
            {
                Id = x.Id,
                Srid = x.Srid,
            })
            .Where(x => newSrids.Contains(x.Srid))
            .ToDictionaryAsync(x => x.Srid, x => x.Id);

        var toCreate = new List<StatisticsOrder>();
        var skipped = 0;
        var localDbSet = appDbContext.Set<StatisticsOrder>().Local;
        foreach (var item in items)
        {
            if (localDbSet.Any(x => x.Srid == item.Srid))
            {
                skipped++;
                continue;
            }

            if (existed.TryGetValue(item.Srid, out var id))
            {
                item.Id = id;

                appDbContext.Set<StatisticsOrder>().Entry(item).State = EntityState.Modified;
                appDbContext.Set<StatisticsOrder>().Entry(item).Property(p => p.LegalEntity).IsModified = false;
            }
            else
            {
                toCreate.Add(item);
            }
        }

        await SaveChanges(toCreate);
        var created = toCreate.Count;
        var updated = items.Count - toCreate.Count - skipped;
        Log.Information($"Invoked {GetType().Name} for '{options.Name}': {created} created; {updated} updated; {skipped} skipped;\n");
    }

    private async Task<List<StatisticsOrder>> GetItems(DateTime dateFrom, int flag = 0)
    {
        var request = new StatisticsOrdersRequest(dateFrom, flag);
        return await apiService.GetOrdersAsync(options.Token, request);
    }

    private async Task SaveChanges(List<StatisticsOrder> items)
    {
        items.EnrichByLegalEntity(options.Name);
        await appDbContext.Set<StatisticsOrder>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }
}