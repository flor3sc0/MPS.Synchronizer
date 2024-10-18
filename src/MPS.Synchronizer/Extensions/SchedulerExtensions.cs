using Coravel;
using MPS.Synchronizer.Application.SynchronizationJobs;
using MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

namespace MPS.Synchronizer.Extensions;

public static class SchedulerExtensions
{
    public static IServiceProvider ConfigureScheduler(this IServiceProvider services)
    {
        services.UseScheduler(scheduler =>
        {
            //scheduler.Schedule<WbPingJob>()
            //    .EveryFifteenMinutes()
            //    .PreventOverlapping(nameof(WbPingJob));

            //scheduler.Schedule<StatisticsIncomesSyncJob>()
            //    .EveryTenSeconds()
            //    .PreventOverlapping(nameof(StatisticsIncomesSyncJob));

            //scheduler.Schedule<StatisticsStocksSyncJob>()
            //    .EveryTenSeconds()
            //    .PreventOverlapping(nameof(StatisticsStocksSyncJob));

            //scheduler.Schedule<StatisticsOrdersSyncJob>()
            //    .EveryTenSeconds()
            //    .PreventOverlapping(nameof(StatisticsOrdersSyncJob));

            scheduler.Schedule<StatisticsSalesSyncJob>()
                .EveryTenSeconds()
                .PreventOverlapping(nameof(StatisticsSalesSyncJob));

            //scheduler.Schedule<StatisticsRealizationReportSyncJob>()
            //    .EveryTenSeconds()
            //    .PreventOverlapping(nameof(StatisticsRealizationReportSyncJob));
        })
        .OnError(exception => throw exception);

        return services;
    }
}