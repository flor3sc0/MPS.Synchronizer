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

public class StatisticsRealizationReportSyncJob(IWbStatisticsApi apiService, AppDbContext appDbContext, LegalEntityOptions options) : IInvocable
{
    public async Task Invoke()
    {
        Log.Information($"Invoke {GetType().Name} for '{options.Name}'");

        var alreadyInit = await appDbContext.Set<StatisticsRealizationReport>().AnyAsync(x => x.LegalEntity == options.Name);
        if (alreadyInit)
        {
            await BaseSync();
            return;
        }

        await InitSync();
    }

    private async Task BaseSync()
    {
        var (monday, sunday) = GetDateOfPreviousPeriod();

        var periodAlreadySync = await appDbContext
            .Set<StatisticsRealizationReport>()
            .AnyAsync(x => x.LegalEntity == options.Name &&
                           x.DateTo == DateOnly.FromDateTime(sunday.Date));

        if (periodAlreadySync)
        {
            Log.Information($"Invoked {GetType().Name} for '{options.Name}'. Period {monday:MM.dd.yyyy} - {sunday:MM.dd.yyyy} already sync\n");
            return;
        }

        var count = await LoadAndSave(monday, sunday);

        Log.Information($"Invoked {GetType().Name} for '{options.Name}' with {count} items\n");
    }

    private async Task InitSync()
    {
        var dateFrom = DateTime.Parse("2024-01-29").Date;
        var dateTo = dateFrom.AddDays(6).Date;
        var totalCounts = 0;
        while (dateTo <= DateTime.Now.Date)
        {
            totalCounts += await LoadAndSave(dateFrom, dateTo);

            dateFrom = dateFrom.AddDays(7);
            dateTo = dateTo.AddDays(7);
        }

        Log.Information($"Invoked [INIT] {GetType().Name} for '{options.Name}' with {totalCounts} items\n");
    }

    private async Task<int> LoadAndSave(DateTime dateFrom, DateTime dateTo)
    {
        var request = new StatisticsRealizationReportRequest(dateFrom, dateTo);
        var totalCounts = 0;

        long rrdId = 0;
        List<StatisticsRealizationReport> items;
        do
        {
            request.Rrdid = rrdId;
            try
            {
                items = await apiService.GetReportDetailByPeriodAsync(options.Token, request) ?? [];
            }
            catch (Exception e)
            {
                Log.Warning($"Большое количество запросов при инициализации {GetType().Name} для '{options.Name}'");
                await Task.Delay(TimeSpan.FromMinutes(1));
                items = await apiService.GetReportDetailByPeriodAsync(options.Token, request) ?? [];
            }

            if (items.Count == 0)
            {
                await Task.Delay(TimeSpan.FromMinutes(1));
                break;
            }

            totalCounts += items.Count;
            rrdId = items.Last().RrdId;

            var delayTask = Task.Delay(TimeSpan.FromMinutes(1));

            await SaveChanges(items);
            await delayTask;

        } while (items.Count >= request.Limit);

        return totalCounts;
    }

    private (DateTime monday, DateTime sunday) GetDateOfPreviousPeriod()
    {
        var now = DateTime.Now;
        var dayOfWeek = now.DayOfWeek;
        var daysBefore = dayOfWeek == DayOfWeek.Sunday ? 7 : (int)dayOfWeek;

        var sunday = now.AddDays(-daysBefore);
        var monday = sunday.AddDays(-6);
        return (monday.Date, sunday.Date);
    }

    private async Task SaveChanges(List<StatisticsRealizationReport> items)
    {
        items.EnrichByLegalEntity(options.Name);
        await appDbContext.Set<StatisticsRealizationReport>().AddRangeAsync(items);
        await appDbContext.SaveChangesAsync();
    }
}