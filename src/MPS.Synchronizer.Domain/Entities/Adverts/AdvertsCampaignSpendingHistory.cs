using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MPS.Synchronizer.Domain.Entities.Adverts;

[Comment("Истории затрат на кампании")]
public class AdvertsCampaignSpendingHistory : BaseSyncEntity
{
    /// <summary>
    /// Идентификатор кампании
    /// </summary>
    [Column("advertId")]
    [JsonPropertyName("advertId")]
    [Comment("Идентификатор кампании")]
    public int AdvertId { get; set; }

    /// <summary>
    /// Номер выставленного документа (при наличии)
    /// </summary>
    [Column("updNum")]
    [JsonPropertyName("updNum")]
    [Comment("Номер выставленного документа (при наличии)")]
    public int UpdNum { get; set; }

    /// <summary>
    /// Время списания
    /// </summary>
    [Column("updTime")]
    [JsonPropertyName("updTime")]
    [Comment("Время списания")]
    public DateTime UpdTime { get; set; }

    /// <summary>
    /// Выставленная сумма
    /// </summary>
    [Column("updSum")]
    [JsonPropertyName("updSum")]
    [Comment("Выставленная сумма")]
    public int UpdSum { get; set; }

    /// <summary>
    /// Название кампании
    /// </summary>
    [Column("campName")]
    [JsonPropertyName("campName")]
    [Comment("Название кампании")]
    public string CampName { get; set; }

    /// <summary>
    /// Тип кампании
    /// </summary>
    [Column("advertType")]
    [JsonPropertyName("advertType")]
    [Comment("Тип кампании")]
    public int AdvertType { get; set; }

    /// <summary>
    /// Источник списания: Баланс, Бонусы, Счет
    /// </summary>
    [Column("paymentType")]
    [JsonPropertyName("paymentType")]
    [Comment("Источник списания: Баланс, Бонусы, Счет")]
    public string PaymentType { get; set; }

    /// <summary>
    /// Статус кампании:
    /// 4 - готова к запуску
    /// 7 - завершена
    /// 8 - отказался
    /// 9 - активна
    /// 11 - приостановлена
    /// </summary>
    [Column("advertStatus")]
    [JsonPropertyName("advertStatus")]
    [Comment("Статус кампании: 4 готова к запуску, 7 завершена, 8 отказался, 9 активна, 11 приостановлена")]
    public int AdvertStatus { get; set; }
}