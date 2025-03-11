using MPS.Synchronizer.Application.ExternalApi.Models.Adverts;
using MPS.Synchronizer.Domain.Entities.Adverts;
using Refit;

namespace MPS.Synchronizer.Application.ExternalApi.Interfaces;

/// <summary>
/// API Продвижения/Маркетинга
/// <para>
/// https://openapi.wb.ru/promotion/api/ru/
/// https://dev.wildberries.ru/openapi/promotion
/// </para>
/// </summary>
public interface IWbAdvertsApi : IWbBaseApi
{
    /// <summary>
    /// Получение списка кампаний
    /// </summary>
    /// <param name="token">Токен авторизации</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список записей</returns>
    [Get("/adv/v1/promotion/count")]
    public Task<AdvertsCampaignsResponse> GetCampaignsAsync([Authorize] string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение истории затрат
    /// </summary>
    /// <param name="token">Токен авторизации</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список записей</returns>
    [Get("/adv/v1/upd")]
    public Task<List<AdvertsCampaignSpendingHistory>> GetCampaignSpendingHistoryAsync(
        [Authorize] string token, [Query] AdvertsCampaignSpendingHistoryRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение информации о кампаниях
    /// </summary>
    /// <param name="token">Токен авторизации</param>
    /// <param name="advertIds">Список идентификаторов кампаний</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список записей</returns>
    [Get("/adv/v1/promotion/adverts")]
    public Task<List<AdvertsCampaignInfoResponse>> GetCampaignInfoAsync(
        [Authorize] string token,
        [Body] IEnumerable<long> advertIds,
        [Query] AdvertsCampaignSpendingHistoryRequest request = default,
        CancellationToken cancellationToken = default);
    //todo заменить квери и добавить бади

    /// <summary>
    /// Возвращает статистику кампаний. Данные вернутся для кампаний в статусах:
    /// 7 — завершено
    /// 9 — приостановлена продавцом
    /// 11 — пауза по расходу бюджета
    /// </summary>
    /// <param name="token">Токен авторизации</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список записей</returns>
    [Get("/adv/v2/fullstats")]
    public Task<List<AdvertsCampaignSpendingHistory>> GetCampaignFullStatsAsync(
        [Authorize] string token, [Query] AdvertsCampaignSpendingHistoryRequest request, CancellationToken cancellationToken = default);
    //todo заменить квери и добавить бади
}