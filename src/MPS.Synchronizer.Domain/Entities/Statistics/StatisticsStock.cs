using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MPS.Synchronizer.Domain.Entities.Statistics;

/// <summary>
/// Информация о остатках товаров на складах WB
/// </summary>
[Comment("Информация о остатках товаров на складах WB")]
public class StatisticsStock : BaseSyncEntity
{
    /// <summary>
    /// Дата и время обновления информации в сервисе.
    /// Это поле соответствует параметру dateFrom в запросе.
    /// Если часовой пояс не указан, то берется Московское время (UTC+3)
    /// </summary>
    [Column("lastChangeDate")]
    [JsonPropertyName("lastChangeDate")]
    [Comment("Дата и время обновления информации в сервисе. Это поле соответствует параметру dateFrom в запросе")]
    public DateTime LastChangeDate { get; set; }

    /// <summary>
    /// Название склада
    /// </summary>
    [Column("warehouseName")]
    [JsonPropertyName("warehouseName")]
    [Comment("Название склада")]
    public string WarehouseName { get; set; }

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
    /// Количество, доступное для продажи (сколько можно добавить в корзину)
    /// </summary>
    [Column("quantity")]
    [JsonPropertyName("quantity")]
    [Comment("Количество, доступное для продажи (сколько можно добавить в корзину)")]
    public long Quantity { get; set; }

    /// <summary>
    /// В пути к клиенту
    /// </summary>
    [Column("inWayToClient")]
    [JsonPropertyName("inWayToClient")]
    [Comment("В пути к клиенту")]
    public long InWayToClient { get; set; }

    /// <summary>
    /// В пути от клиента
    /// </summary>
    [Column("inWayFromClient")]
    [JsonPropertyName("inWayFromClient")]
    [Comment("В пути от клиента")]
    public long InWayFromClient { get; set; }

    /// <summary>
    /// Полное (непроданное) количество, которое числится за складом (= quantity + в пути)
    /// </summary>
    [Column("quantityFull")]
    [JsonPropertyName("quantityFull")]
    [Comment("Полное (непроданное) количество, которое числится за складом (= quantity + в пути)")]
    public long QuantityFull { get; set; }

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
    /// Размер
    /// </summary>
    [Column("techSize")]
    [JsonPropertyName("techSize")]
    [Comment("Размер")]
    public string TechSize { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    [Column("Price")]
    [JsonPropertyName("Price")]
    [Comment("Цена")]
    public decimal Price { get; set; }

    /// <summary>
    /// Скидка
    /// </summary>
    [Column("Discount")]
    [JsonPropertyName("Discount")]
    [Comment("Скидка")]
    public decimal Discount { get; set; }

    /// <summary>
    /// Договор поставки (внутренние технологические данные)
    /// </summary>
    [Column("isSupply")]
    [JsonPropertyName("isSupply")]
    [Comment("Договор поставки (внутренние технологические данные)")]
    public bool IsSupply { get; set; }

    /// <summary>
    /// Договор реализации (внутренние технологические данные)
    /// </summary>
    [Column("isRealization")]
    [JsonPropertyName("isRealization")]
    [Comment("Договор реализации (внутренние технологические данные)")]
    public bool IsRealization { get; set; }

    /// <summary>
    /// Код контракта (внутренние технологические данные)
    /// </summary>
    [Column("SCCode")]
    [JsonPropertyName("SCCode")]
    [Comment("Код контракта (внутренние технологические данные)")]
    public string SCCode { get; set; }
}