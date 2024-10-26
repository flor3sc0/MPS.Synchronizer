using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MPS.Synchronizer.Domain.Entities.Statistics;

/// <summary>
/// Информация о продаже или возврате товара.
/// </summary>
[Comment("Информация о продаже или возврате товара")]
public class StatisticsSale : BaseSyncEntity
{
    /// <summary>
    /// Дата и время продажи.
    /// Это поле соответствует параметру dateFrom в запросе, если параметр flag=1.
    /// Если часовой пояс не указан, то берется Московское время (UTC+3)
    /// </summary>
    [Column("date")]
    [JsonPropertyName("date")]
    [Comment("Дата и время продажи. Это поле соответствует параметру dateFrom в запросе, если параметр flag=1")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Дата и время обновления информации в сервисе.
    /// Это поле соответствует параметру dateFrom в запросе, если параметр flag=0 или не указан.
    /// Если часовой пояс не указан, то берется Московское время (UTC+3)
    /// </summary>
    [Column("lastChangeDate")]
    [JsonPropertyName("lastChangeDate")]
    [Comment("Дата и время обновления информации в сервисе. Это поле соответствует параметру dateFrom в запросе, если параметр flag=0 или не указан")]
    public DateTime LastChangeDate { get; set; }

    /// <summary>
    /// Склад отгрузки
    /// </summary>
    [Column("warehouseName")]
    [JsonPropertyName("warehouseName")]
    [Comment("Склад отгрузки")]
    public string WarehouseName { get; set; }

    /// <summary>
    /// Страна
    /// </summary>
    [Column("countryName")]
    [JsonPropertyName("countryName")]
    [Comment("Страна")]
    public string CountryName { get; set; }

    /// <summary>
    /// Округ
    /// </summary>
    [Column("oblastOkrugName")]
    [JsonPropertyName("oblastOkrugName")]
    [Comment("Округ")]
    public string OblastOkrugName { get; set; }

    /// <summary>
    /// Регион
    /// </summary>
    [Column("regionName")]
    [JsonPropertyName("regionName")]
    [Comment("Регион")]
    public string RegionName { get; set; }

    /// <summary>
    /// Артикул продавца
    /// </summary>
    [Column("supplierArticle")]
    [JsonPropertyName("supplierArticle")]
    [Comment("Артикул продавца")]
    public string SupplierArticle { get; set; }

    /// <summary>
    /// Артикул WB
    /// </summary>
    [Column("nmId")]
    [JsonPropertyName("nmId")]
    [Comment("Артикул WB")]
    public long NmId { get; set; }

    /// <summary>
    /// Баркод
    /// </summary>
    [Column("barcode")]
    [JsonPropertyName("barcode")]
    [Comment("Баркод")]
    public string Barcode { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    [Column("category")]
    [JsonPropertyName("category")]
    [Comment("Категория")]
    public string Category { get; set; }

    /// <summary>
    /// Предмет
    /// </summary>
    [Column("subject")]
    [JsonPropertyName("subject")]
    [Comment("Предмет")]
    public string Subject { get; set; }

    /// <summary>
    /// Бренд
    /// </summary>
    [Column("brand")]
    [JsonPropertyName("brand")]
    [Comment("Бренд")]
    public string Brand { get; set; }

    /// <summary>
    /// Размер товара
    /// </summary>
    [Column("techSize")]
    [JsonPropertyName("techSize")]
    [Comment("Размер товара")]
    public string TechSize { get; set; }

    /// <summary>
    /// Номер поставки
    /// </summary>
    [Column("incomeID")]
    [JsonPropertyName("incomeID")]
    [Comment("Номер поставки")]
    public long IncomeId { get; set; }

    /// <summary>
    /// Договор поставки
    /// </summary>
    [Column("isSupply")]
    [JsonPropertyName("isSupply")]
    [Comment("Договор поставки")]
    public bool IsSupply { get; set; }

    /// <summary>
    /// Договор реализации
    /// </summary>
    [Column("isRealization")]
    [JsonPropertyName("isRealization")]
    [Comment("Договор реализации")]
    public bool IsRealization { get; set; }

    /// <summary>
    /// Цена без скидок
    /// </summary>
    [Column("totalPrice")]
    [JsonPropertyName("totalPrice")]
    [Comment("Цена без скидок")]
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Скидка продавца
    /// </summary>
    [Column("discountPercent")]
    [JsonPropertyName("discountPercent")]
    [Comment("Скидка продавца")]
    public long DiscountPercent { get; set; }

    /// <summary>
    /// Скидка WB
    /// </summary>
    [Column("spp")]
    [JsonPropertyName("spp")]
    [Comment("Скидка WB")]
    public decimal Spp { get; set; }

    /// <summary>
    /// Оплачено с WB Кошелька
    /// </summary>
    [Column("paymentSaleAmount")]
    [JsonPropertyName("paymentSaleAmount")]
    [Comment("Оплачено с WB Кошелька")]
    public long PaymentSaleAmount { get; set; }

    /// <summary>
    /// К перечислению продавцу
    /// </summary>
    [Column("forPay")]
    [JsonPropertyName("forPay")]
    [Comment("К перечислению продавцу")]
    public decimal ForPay { get; set; }

    /// <summary>
    /// Фактическая цена с учетом всех скидок (к взиманию с покупателя)
    /// </summary>
    [Column("finishedPrice")]
    [JsonPropertyName("finishedPrice")]
    [Comment("Фактическая цена с учетом всех скидок (к взиманию с покупателя)")]
    public decimal FinishedPrice { get; set; }

    /// <summary>
    /// Цена со скидкой продавца, от которой считается сумма к перечислению продавцу forPay (= totalPrice * (1 - discountPercent/100))
    /// </summary>
    [Column("priceWithDisc")]
    [JsonPropertyName("priceWithDisc")]
    [Comment("Цена со скидкой продавца, от которой считается сумма к перечислению продавцу forPay (= totalPrice * (1 - discountPercent/100))")]
    public decimal PriceWithDisc { get; set; }

    /// <summary>
    /// Уникальный идентификатор продажи/возврата
    /// </summary>
    [Column("saleID")]
    [JsonPropertyName("saleID")]
    [Comment("Уникальный идентификатор продажи/возврата")]
    public string SaleId { get; set; }

    /// <summary>
    /// Тип заказа:
    /// Клиентский — заказ, поступивший от покупателя
    /// Возврат Брака — возврат товара продавцу
    /// Принудительный возврат — возврат товара продавцу
    /// Возврат обезлички — возврат товара продавцу
    /// Возврат Неверного Вложения — возврат товара продавцу
    /// Возврат Продавца — возврат товара продавцу
    /// Возврат из Отзыва — возврат товара продавцу
    /// АвтоВозврат МП — возврат товара продавцу
    /// Недокомплект (Вина продавца) — возврат товара продавцу
    /// Возврат КГТ — возврат товара продавцу
    /// </summary>
    [Column("orderType")]
    [JsonPropertyName("orderType")]
    [Comment("Тип заказа: Клиентский/Возврат Брака/Принудительный возврат/Возврат обезлички/Возврат Неверного Вложения/Возврат Продавца/Возврат из Отзыва/АвтоВозврат МП/Недокомплект (Вина продавца)/Возврат КГТ")]
    public string OrderType { get; set; }

    /// <summary>
    /// Идентификатор стикера
    /// </summary>
    [Column("sticker")]
    [JsonPropertyName("sticker")]
    [Comment("Идентификатор стикера")]
    public string Sticker { get; set; }

    /// <summary>
    /// Номер заказа
    /// </summary>
    [Column("gNumber")]
    [JsonPropertyName("gNumber")]
    [Comment("Номер заказа")]
    public string GNumber { get; set; }

    /// <summary>
    /// Уникальный идентификатор заказа.
    /// Примечание для использующих API Маркетплейс: srid равен rid в ответах методов сборочных заданий.
    /// </summary>
    [Column("srid")]
    [JsonPropertyName("srid")]
    [Comment("Уникальный идентификатор заказа. Примечание для использующих API Маркетплейс: srid равен rid в ответах методов сборочных заданий")]
    public string Srid { get; set; }
}