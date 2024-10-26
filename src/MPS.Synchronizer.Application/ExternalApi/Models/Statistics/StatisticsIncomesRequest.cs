using Refit;

namespace MPS.Synchronizer.Application.ExternalApi.Models.Statistics;

public class StatisticsIncomesRequest(DateTime dateFrom)
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
}