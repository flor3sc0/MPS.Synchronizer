using Refit;

namespace MPS.Synchronizer.Application.ExternalApi.Models.Statistics
{
    public class StatisticsRealizationReportRequest(DateTime dateFrom, DateTime dateTo, int rrdid = 0, int limit = 30_000)
    {
        /// <summary>
        /// Дата и время последнего изменения по поставке.
        /// Дата в формате RFC3339.Можно передать дату или дату со временем.
        /// Время можно указывать с точностью до секунд или миллисекунд. Время передаётся в часовом поясе Мск (UTC+3).
        /// Примеры:
        /// 2019-06-20
        /// 2019-06-20T23:59:59
        /// 2019-06-20T00:00:00.12345
        /// 2017-03-25T00:00:00
        /// </summary>
        [Query(Format = "yyyy-MM-dd")]
        [AliasAs("dateFrom")]
        public DateTime DateFrom { get; init; } = dateFrom;
        /// <summary>
        /// Конечная дата отчета
        /// </summary>
        [Query(Format = "yyyy-MM-dd")]
        [AliasAs("dateTo")]
        public DateTime DateTo { get; init; } = dateTo;

        /// <summary>
        /// Default: 100000
        /// Максимальное количество строк отчета, возвращаемых методом.Не может быть более 100000.
        /// </summary>
        [AliasAs("limit")]
        public long Limit { get; set; } = limit;

        /// <summary>
        /// Уникальный идентификатор строки отчета.Необходим для получения отчета частями.
        /// Загрузку отчета нужно начинать с rrdid = 0 и при последующих вызовах API передавать в запросе значение rrd_id из последней строки, полученной в результате предыдущего вызова.
        /// Таким образом для загрузки одного отчета может понадобиться вызывать API до тех пор, пока количество возвращаемых строк не станет равным нулю.
        /// </summary>
        [AliasAs("rrdid")]
        public long Rrdid { get; set; } = rrdid;
    }
}