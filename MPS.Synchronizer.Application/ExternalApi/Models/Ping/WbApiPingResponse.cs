namespace MPS.Synchronizer.Application.ExternalApi.Models.Ping;

/// <summary>
/// Ответ на Ping запрос.
/// </summary>
public class WbApiPingResponse
{
    /// <summary>
    /// Временная метка запроса
    /// </summary>
    public DateTime Ts { get; set; }

    /// <summary>
    /// Статус. Возможное значение: "OK"
    /// </summary>
    public string Status { get; set; }
}