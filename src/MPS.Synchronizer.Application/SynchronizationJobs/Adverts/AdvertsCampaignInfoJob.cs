using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using MPS.Synchronizer.Application.CommonModels;
using MPS.Synchronizer.Application.Extensions;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Application.ExternalApi.Models.Adverts;
using MPS.Synchronizer.Domain.Entities.Adverts;
using MPS.Synchronizer.Persistence;
using Serilog;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Adverts;


//todo переписать
//проходишь по advertid и перезаписываешь таблицу ежедневно
public class AdvertsCampaignInfo(IWbAdvertsApi apiService, AppDbContext appDbContext, LegalEntityOptions options) : IInvocable
{
    public async Task Invoke()
    {
        Log.Information($"Invoke {GetType().Name} for '{options.Name}'");

        var alreadyInit = await appDbContext.Set<AdvertsCampaignSpendingHistory>().AnyAsync(x => x.LegalEntity == options.Name);
        if (alreadyInit)
        {
            await BaseSync();
            return;
        }

        await InitSync();
    }

    private async Task BaseSync()
    {
        var date = DateTime.Now.AddDays(-1).Date;
        var count = await LoadAndSave(date, date);

        Log.Information($"Invoked {GetType().Name} for '{options.Name}' with {count} items\n");
    }

    private async Task InitSync()
    {
        var dateFrom = DateTime.Parse("2024-01-01").Date;
        var daysInMonth = DateTime.DaysInMonth(dateFrom.Year, dateFrom.Month);
        var dateTo = dateFrom.AddDays(daysInMonth - 1).Date;

        var totalCounts = 0;
        while (dateTo <= DateTime.Now.Date)
        {
            totalCounts += await LoadAndSave(dateFrom, dateTo);

            dateFrom = dateFrom.AddDays(daysInMonth);
            daysInMonth = DateTime.DaysInMonth(dateFrom.Year, dateFrom.Month);
            dateTo = dateFrom.AddDays(daysInMonth - 1).Date;
        }

        Log.Information($"Invoked [INIT] {GetType().Name} for '{options.Name}' with {totalCounts} items\n");
    }

    private async Task<int> LoadAndSave(DateTime dateFrom, DateTime dateTo)
    {
        var request = new AdvertsCampaignSpendingHistoryRequest(dateFrom, dateTo);
        var items = await apiService.GetCampaignSpendingHistoryAsync(options.Token, request);

        var delayTask = Task.Delay(TimeSpan.FromMinutes(1));
        await SaveChanges(items);
        await delayTask;

        return items.Count;
    }

    private async Task SaveChanges(List<AdvertsCampaignSpendingHistory> items)
    {
        items.EnrichByLegalEntity(options.Name);
        await appDbContext.Set<AdvertsCampaignSpendingHistory>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }
}