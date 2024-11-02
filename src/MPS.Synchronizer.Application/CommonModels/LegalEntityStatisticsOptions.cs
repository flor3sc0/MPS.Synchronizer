using System.ComponentModel.DataAnnotations;

namespace MPS.Synchronizer.Application.CommonModels
{
    public class LegalEntityStatisticsOptions
    {
        //[Required(AllowEmptyStrings = false)]
        //public string PingJobCron { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int StatisticsIncomesSyncJobHour { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int StatisticsIncomesSyncJobMinute { get; set; }


        [Required(AllowEmptyStrings = false)]
        public int StatisticsStocksSyncJobHour { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int StatisticsStocksSyncJobMinute { get; set; }


        [Required(AllowEmptyStrings = false)]
        public int StatisticsOrdersSyncJobHour { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int StatisticsOrdersSyncJobMinute { get; set; }


        [Required(AllowEmptyStrings = false)]
        public int StatisticsSalesSyncJobHour { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int StatisticsSalesSyncJobMinute { get; set; }


        [Required(AllowEmptyStrings = false)]
        public int StatisticsRealizationReportSyncJobHour { get; set; }

        [Required(AllowEmptyStrings = false)]
        public int StatisticsRealizationReportSyncJobMinute { get; set; }
    }
}