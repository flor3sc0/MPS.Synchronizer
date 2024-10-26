using Coravel;
using Microsoft.Extensions.Options;
using MPS.Synchronizer.Application.Settings;
using MPS.Synchronizer.Application.SynchronizationJobs;
using MPS.Synchronizer.Application.SynchronizationJobs.Statistics;

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
                //scheduler.ScheduleWithParams<WbPingJob>(le)
                //    .Cron(le.PingJobCron)
                //    //.EveryTenSeconds()
                //    .PreventOverlapping($"{nameof(WbPingJob)}_{le.Name}");

                scheduler.ScheduleWithParams<StatisticsIncomesSyncJob>(le)
                    //.Cron(le.StatisticsIncomesSyncJobCron)
                    .EveryTenSeconds()
                    .PreventOverlapping($"{nameof(StatisticsIncomesSyncJob)}_{le.Name}");

                scheduler.ScheduleWithParams<StatisticsStocksSyncJob>(le)
                    //.Cron(le.StatisticsStocksSyncJobCron)
                    .EveryTenSeconds()
                    .PreventOverlapping($"{nameof(StatisticsStocksSyncJob)}_{le.Name}");

                scheduler.ScheduleWithParams<StatisticsOrdersSyncJob>(le)
                    //.Cron(le.StatisticsStocksSyncJobCron)
                    .EveryTenSeconds()
                    .PreventOverlapping($"{nameof(StatisticsOrdersSyncJob)}_{le.Name}");

                scheduler.ScheduleWithParams<StatisticsSalesSyncJob>(le)
                    //.Cron(le.StatisticsSalesSyncJobCron)
                    .EveryTenSeconds()
                    .PreventOverlapping($"{nameof(StatisticsSalesSyncJob)}_{le.Name}");

                scheduler.ScheduleWithParams<StatisticsRealizationReportSyncJob>(le)
                    //.Cron(le.StatisticsRealizationReportSyncJobCron)
                    .EveryTenSeconds()
                    .PreventOverlapping($"{nameof(StatisticsRealizationReportSyncJob)}_{le.Name}");
            }
        });
        //.OnError(exception => throw exception); // todo delete

        return services;
    }
}