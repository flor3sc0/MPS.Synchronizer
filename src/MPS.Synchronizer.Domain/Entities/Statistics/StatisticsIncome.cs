using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MPS.Synchronizer.Domain.Entities.Statistics;


/// <summary>
/// Информация о поставке.
/// </summary>
[Comment("Информация о поставке")]
public class StatisticsIncome : BaseSyncEntity
{
    /// <summary>
    /// Номер поставки
    /// </summary>
    [Column("incomeId")]
    [JsonPropertyName("incomeId")]
    [Comment("Номер поставки")]
    public int IncomeId { get; set; }

    /// <summary>
    /// Номер УПД
    /// </summary>
    [Column("number")]
    [JsonPropertyName("number")]
    [Comment("Номер УПД")]
    public string Number { get; set; }

    /// <summary>
    /// Дата поступления.
    /// Если часовой пояс не указан, то берется Московское время UTC+3
    /// </summary>
    [Column("date")]
    [JsonPropertyName("date")]
    [Comment("Дата поступления")]
    public DateOnly Date { get; set; }

    /// <summary>
    /// Дата и время обновления информации в сервисе. Это поле соответствует параметру dateFrom в запросе.
    /// Если часовой пояс не указан, то берется Московское время UTC+3
    /// </summary>
    [Column("lastChangeDate")]
    [JsonPropertyName("lastChangeDate")]
    [Comment("Дата и время обновления информации в сервисе. Это поле соответствует параметру dateFrom в запросе")]
    public DateTime LastChangeDate { get; set; }

    /// <summary>
    /// Артикул продавца
    /// </summary>
    [Column("supplierArticle")]
    [JsonPropertyName("supplierArticle")]
    [Comment("Артикул продавца")]
    public string SupplierArticle { get; set; }

    /// <summary>
    /// Размер товара (пример S, M, L, XL, 42, 42-43)
    /// </summary>
    [Column("techSize")]
    [JsonPropertyName("techSize")]
    [Comment("Размер товара (пример S, M, L, XL, 42, 42-43)")]
    public string TechSize { get; set; }

    /// <summary>
    /// Бар-код
    /// </summary>
    [Column("barcode")]
    [JsonPropertyName("barcode")]
    [Comment("Бар-код")]
    public string Barcode { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    [Column("quantity")]
    [JsonPropertyName("quantity")]
    [Comment("Количество")]
    public int Quantity { get; set; }

    /// <summary>
    /// Цена из УПД
    /// </summary>
    [Column("totalPrice")]
    [JsonPropertyName("totalPrice")]
    [Comment("Цена из УПД")]
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Дата принятия (закрытия) в WB.
    /// Если часовой пояс не указан, то берется Московское время UTC+3
    /// </summary>
    [Column("dateClose")]
    [JsonPropertyName("dateClose")]
    [Comment("Дата принятия (закрытия) в WB")]
    public DateOnly DateClose { get; set; }

    /// <summary>
    /// Название склада
    /// </summary>
    [Column("warehouseName")]
    [JsonPropertyName("warehouseName")]
    [Comment("Название склада")]
    public string WarehouseName { get; set; }

    /// <summary>
    /// Артикул WB
    /// </summary>
    [Column("nmId")]
    [JsonPropertyName("nmId")]
    [Comment("Артикул WB")]
    public long NmId { get; set; }

    /// <summary>
    /// Текущий статус поставки
    /// </summary>
    [Column("status")]
    [JsonPropertyName("status")]
    [Comment("Текущий статус поставки")]
    public string Status { get; set; }
}