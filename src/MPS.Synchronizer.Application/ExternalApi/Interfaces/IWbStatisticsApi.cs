using MPS.Synchronizer.Application.ExternalApi.Models.Statistics;
using MPS.Synchronizer.Domain.Entities.Statistics;
using Refit;

namespace MPS.Synchronizer.Application.ExternalApi.Interfaces;

/// <summary>
/// API Статистики
/// <para>
/// https://openapi.wb.ru/statistics/api/ru/
/// </para>
/// </summary>
public interface IWbStatisticsApi : IWbBaseApi
{
    /// <summary>
    /// Получение списка Поставок по дате
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Список записей</returns>
    [Get("/api/v1/supplier/incomes")]
    public Task<List<StatisticsIncome>> GetIncomesAsync(
        [Authorize] string token, [Query] StatisticsIncomesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает остатки товаров на складах WB.
    /// Данные обновляются раз в 30 минут.
    /// Сервис статистики не хранит историю остатков товаров, поэтому получить данные о них можно только на текущий момент
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Список записей</returns>
    [Get("/api/v1/supplier/stocks")]
    public Task<List<StatisticsStock>> GetStocksAsync(
        [Authorize] string token, [Query] StatisticsStocksRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает заказы.
    /// Данные обновляются раз в 30 минут.
    /// 1 строка = 1 заказ = 1 единица товара.
    /// Для определения заказа рекомендуем использовать поле srid.
    /// Данные заказа хранятся 90 дней от даты заказа
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Список записей</returns>
    [Get("/api/v1/supplier/orders")]
    public Task<List<StatisticsOrder>> GetOrdersAsync(
        [Authorize] string token, [Query] StatisticsOrdersRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Продажи и возвраты.
    /// Гарантируется хранение данных не более 90 дней от даты продажи.
    /// Данные обновляются раз в 30 минут.
    /// Для идентификации заказа следует использовать поле srid.
    /// 1 строка = 1 продажа/возврат = 1 единица товара.
    /// Максимум 1 запрос в минуту
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Список записей</returns>
    [Get("/api/v1/supplier/sales")]
    public Task<List<StatisticsSale>> GetSalesAsync(
        [Authorize] string token, [Query] StatisticsSalesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Детализация к еженедельному отчёту реализации.
    /// Доступны данные, начиная с 29 января 2024 года.
    /// Максимум 1 запрос в минуту.
    /// Если нет данных за указанный период, метод вернет []
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Список записей</returns>
    [Get("/api/v5/supplier/reportDetailByPeriod")]
    public Task<List<StatisticsRealizationReport>> GetReportDetailByPeriodAsync(
        [Authorize] string token, [Query] StatisticsRealizationReportRequest request, CancellationToken cancellationToken = default);
}