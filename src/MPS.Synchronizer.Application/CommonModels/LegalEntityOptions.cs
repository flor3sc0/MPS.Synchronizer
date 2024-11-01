using System.ComponentModel.DataAnnotations;

namespace MPS.Synchronizer.Application.CommonModels;

public class LegalEntityOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Token { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string PingJobCron { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string StatisticsIncomesSyncJobCron { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string StatisticsStocksSyncJobCron { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string StatisticsOrdersSyncJobCron { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string StatisticsSalesSyncJobCron { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string StatisticsRealizationReportSyncJobCron { get; set; }
}