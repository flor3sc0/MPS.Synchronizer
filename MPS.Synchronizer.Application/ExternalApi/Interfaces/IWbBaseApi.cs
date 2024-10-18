using MPS.Synchronizer.Application.ExternalApi.Models.Ping;
using Refit;

namespace MPS.Synchronizer.Application.ExternalApi.Interfaces;

/// <summary>
/// Базовый интерфейс для WB API
/// </summary>
public interface IWbBaseApi
{
    /// <summary>
    /// Пинг
    /// </summary>
    /// <param name="cancellationToken">Токен</param>
    [Get("/ping")]
    public Task<WbApiPingResponse> PingAsync(CancellationToken cancellationToken = default);
}