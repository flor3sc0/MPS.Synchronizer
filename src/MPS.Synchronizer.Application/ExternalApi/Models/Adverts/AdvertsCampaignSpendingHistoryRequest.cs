using Refit;

namespace MPS.Synchronizer.Application.ExternalApi.Models.Adverts;


public class AdvertsCampaignSpendingHistoryRequest(DateTime from, DateTime to)
{
    /// <summary>
    /// Начало интервала
    /// </summary>
    [Query(Format = "yyyy-MM-dd")]
    [AliasAs("from")]
    public DateTime From { get; init; } = from;

    /// <summary>
    /// Конец интервала.
    /// (Минимальный интервал 1 день, максимальный 31)
    /// </summary>
    [Query(Format = "yyyy-MM-dd")]
    [AliasAs("to")]
    public DateTime To { get; init; } = to;
}