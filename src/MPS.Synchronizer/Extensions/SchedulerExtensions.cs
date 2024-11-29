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

        services.UseScheduler(scheduler =>
            {
                foreach (var le in legalEntityOptions)
                {
                    scheduler.OnWorker("StatisticsSimple");

                    scheduler.ScheduleWithParams<WbPingJob>(le)
                        .DailyAt(18, 30)
                        .Zoned(TimeZoneInfo.Local)
                        .RunOnceAtStart()
                        .PreventOverlapping($"{nameof(WbPingJob)}_{le.Name}");

                    scheduler.ScheduleWithParams<StatisticsIncomesSyncJob>(le)
                        .DailyAt(le.Statistics.StatisticsIncomesSyncJobHour, le.Statistics.StatisticsIncomesSyncJobMinute)
                        .Zoned(TimeZoneInfo.Local)
                        .RunOnceAtStart()
                        .PreventOverlapping($"{nameof(StatisticsIncomesSyncJob)}_{le.Name}");

                    scheduler.ScheduleWithParams<StatisticsStocksSyncJob>(le)
                        .DailyAt(le.Statistics.StatisticsStocksSyncJobHour, le.Statistics.StatisticsStocksSyncJobMinute)
                        .Zoned(TimeZoneInfo.Local)
                        .RunOnceAtStart()
                        .PreventOverlapping($"{nameof(StatisticsStocksSyncJob)}_{le.Name}");

                    scheduler.OnWorker("Statistics");

                    scheduler.ScheduleWithParams<StatisticsOrdersSyncJob>(le)
                        .DailyAt(le.Statistics.StatisticsOrdersSyncJobHour, le.Statistics.StatisticsOrdersSyncJobMinute)
                        .Zoned(TimeZoneInfo.Local)
                        .RunOnceAtStart()
                        .PreventOverlapping($"{nameof(StatisticsOrdersSyncJob)}_{le.Name}");

                    scheduler.ScheduleWithParams<StatisticsSalesSyncJob>(le)
                        .DailyAt(le.Statistics.StatisticsSalesSyncJobHour, le.Statistics.StatisticsSalesSyncJobMinute)
                        .Zoned(TimeZoneInfo.Local)
                        .RunOnceAtStart()
                        .PreventOverlapping($"{nameof(StatisticsSalesSyncJob)}_{le.Name}");

                    scheduler.ScheduleWithParams<StatisticsRealizationReportSyncJob>(le)
                        .DailyAt(le.Statistics.StatisticsRealizationReportSyncJobHour, le.Statistics.StatisticsRealizationReportSyncJobMinute)
                        .Zoned(TimeZoneInfo.Local)
                        .RunOnceAtStart()
                        .PreventOverlapping($"{nameof(StatisticsRealizationReportSyncJob)}_{le.Name}");
                }
            })
            .OnError(exception => Log.Error("[Coravel]: {Exception}", exception));

        return services;
    }
}