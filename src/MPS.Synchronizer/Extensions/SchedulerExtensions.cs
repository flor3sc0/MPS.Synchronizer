using Coravel;
using Microsoft.Extensions.Options;
using MPS.Synchronizer.Application.CommonModels;
using MPS.Synchronizer.Application.SynchronizationJobs;
using MPS.Synchronizer.Application.SynchronizationJobs.Statistics;
using Serilog;

namespace MPS.Synchronizer.Extensions;

public static class SchedulerExtensions
{
    public static IServiceProvider ConfigureScheduler(this IServiceProvider services)
    {
        var legalEntityOptions = services.GetService<IOptions<WbOptions>>().Value.LegalEntities;

        //todo добавить воркеры
        services.UseScheduler(scheduler =>
        {
            foreach (var le in legalEntityOptions)
            {
                //scheduler.ScheduleWithParams<WbPingJob>(le)
                //    //.Cron(le.PingJobCron)
                //    .EveryTenSeconds()
                //    .PreventOverlapping($"{nameof(WbPingJob)}_{le.Name}");

                scheduler.ScheduleWithParams<StatisticsIncomesSyncJob>(le)
                    //.Cron(le.StatisticsIncomesSyncJobCron)
                    .DailyAt(05, 00)
                    .Zoned(TimeZoneInfo.Local)
                    //.RunOnceAtStart()
                    .PreventOverlapping($"{nameof(StatisticsIncomesSyncJob)}_{le.Name}");

                scheduler.ScheduleWithParams<StatisticsStocksSyncJob>(le)
                    //.Cron(le.StatisticsStocksSyncJobCron)
                    .DailyAt(05, 05)
                    .Zoned(TimeZoneInfo.Local)
                    //.RunOnceAtStart()
                    .PreventOverlapping($"{nameof(StatisticsStocksSyncJob)}_{le.Name}");

                scheduler.ScheduleWithParams<StatisticsOrdersSyncJob>(le)
                    //.Cron(le.StatisticsStocksSyncJobCron)
                    .DailyAt(05, 44)
                    .Zoned(TimeZoneInfo.Local)
                    .RunOnceAtStart()
                    .PreventOverlapping($"{nameof(StatisticsOrdersSyncJob)}_{le.Name}");

                scheduler.ScheduleWithParams<StatisticsSalesSyncJob>(le)
                    //.Cron(le.StatisticsSalesSyncJobCron)
                    .DailyAt(05, 55)
                    .Zoned(TimeZoneInfo.Local)
                    //.RunOnceAtStart()
                    .PreventOverlapping($"{nameof(StatisticsSalesSyncJob)}_{le.Name}");

                scheduler.ScheduleWithParams<StatisticsRealizationReportSyncJob>(le)
                     //.Cron(le.StatisticsRealizationReportSyncJobCron)
                     .DailyAt(05, 33)
                     .Zoned(TimeZoneInfo.Local)
                     //.RunOnceAtStart()
                     .PreventOverlapping($"{nameof(StatisticsRealizationReportSyncJob)}_{le.Name}");
            }
        })
        .OnError(exception => Log.Error(exception, "")); // todo delete

        return services;
    }
}