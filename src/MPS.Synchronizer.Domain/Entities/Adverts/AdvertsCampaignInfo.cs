using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MPS.Synchronizer.Domain.Entities.Adverts;

public class AdvertsCampaignInfo
{
    /// <summary>ID кампании</summary>
    [Column("advertId")]
    [JsonPropertyName("advertId")]
    [Comment("ID кампании")]
    public int AdvertId { get; set; }

    /// <summary>Время создания кампании</summary>
    [Column("createTime")]
    [JsonPropertyName("createTime")]
    [Comment("Время создания кампании")]
    public DateTime CreateTime { get; set; }

    /// <summary>Дата последнего запуска кампании</summary>
    [Column("startTime")]
    [JsonPropertyName("startTime")]
    [Comment("Дата последнего запуска кампании")]
    public DateTime StartTime { get; set; }

    /// <summary>Время последнего изменения кампании</summary>
    [Column("changeTime")]
    [JsonPropertyName("changeTime")]
    [Comment("Время последнего изменения кампании")]
    public DateTime ChangeTime { get; set; }

    /// <summary>Дата завершения кампании</summary>
    [Column("endTime")]
    [JsonPropertyName("endTime")]
    [Comment("Дата завершения кампании")]
    public DateTime EndTime { get; set; }

    /// <summary>Название кампании</summary>
    [Column("name")]
    [JsonPropertyName("name")]
    [Comment("Название кампании")]
    public string Name { get; set; }

    /// <summary>Дневной бюджет, если не установлен, то 0</summary>
    [Column("dailyBudget")]
    [JsonPropertyName("dailyBudget")]
    [Comment("Дневной бюджет, если не установлен, то 0")]
    public int DailyBudget { get; set; }

    /// <summary>
    /// Статус кампании:
    /// 1 - кампания в процессе удаления
    /// 4 - готова к запуску
    /// 7 - Кампания завершена
    /// 8 - отказался
    /// 9 - идут показы
    /// 11 - Кампания на паузе
    /// </summary>
    [Column("status")]
    [JsonPropertyName("status")]
    [Comment("Статус кампании")]
    public int Status { get; set; }

    /// <summary>
    /// Тип кампании:
    /// 4 - кампания в каталоге (устаревший тип)
    /// 5 - кампания в карточке товара (устаревший тип)
    /// 6 - кампания в поиске (устаревший тип)
    /// 7 - кампания в рекомендациях на главной странице (устаревший тип)
    /// </summary>
    [Column("type")]
    [JsonPropertyName("type")]
    [Comment("Тип кампании")]
    public int Type { get; set; }

    /// <summary>
    /// Модель оплаты:
    /// cpm — за показы
    /// cpo — за заказы
    /// </summary>
    [Column("paymentType")]
    [JsonPropertyName("paymentType")]
    [Comment("Модель оплаты")]
    public string PaymentType { get; set; } = string.Empty;

    /// <summary>
    /// Активность фиксированных фраз:
    /// false — не активны
    /// true — активны
    /// </summary>
    [Column("searchPluseState")]
    [JsonPropertyName("searchPluseState")]
    [Comment("Активность фиксированных фраз")]
    public bool SearchPluseState { get; set; }

    /// <summary>Параметры кампании</summary>
    [Column("params")]
    [JsonPropertyName("params")]
    [Comment("Параметры кампании")]
    public List<CampaignParam> Params { get; set; } = new();
}

public class CampaignParam
{
    /// <summary>Название предметной группы</summary>
    [Column("subjectName")]
    [JsonPropertyName("subjectName")]
    [Comment("Название предметной группы")]
    public string SubjectName { get; set; }

    /// <summary>Флаг активности предметной группы</summary>
    [Column("active")]
    [JsonPropertyName("active")]
    [Comment("Флаг активности предметной группы")]
    public bool Active { get; set; }

    /// <summary>Интервалы часов показа кампании</summary>
    [Column("intervals")]
    [JsonPropertyName("intervals")]
    [Comment("Интервалы часов показа кампании")]
    public List<Interval> Intervals { get; set; } = new();

    /// <summary>Текущая ставка</summary>
    [Column("price")]
    [JsonPropertyName("price")]
    [Comment("Текущая ставка")]
    public int Price { get; set; }

    /// <summary>ID предметной группы</summary>
    [Column("subjectId")]
    [JsonPropertyName("subjectId")]
    [Comment("ID предметной группы")]
    public int SubjectId { get; set; }

    /// <summary>Массив карточек товаров кампании</summary>
    [Column("nms")]
    [JsonPropertyName("nms")]
    [Comment("Массив карточек товаров кампании")]
    public List<Nm> Nms { get; set; } = new();
}

public class Interval
{
    /// <summary>Начало интервала</summary>
    [Column("begin")]
    [JsonPropertyName("begin")]
    [Comment("Начало интервала")]
    public int Begin { get; set; }

    /// <summary>Конец интервала</summary>
    [Column("end")]
    [JsonPropertyName("end")]
    [Comment("Конец интервала")]
    public int End { get; set; }
}

public class Nm
{
    /// <summary>Числовой ID карточки товара WB (nmId)</summary>
    [Column("nm")]
    [JsonPropertyName("nm")]
    [Comment("Числовой ID карточки товара WB (nmId)")]
    public int NmId { get; set; }

    /// <summary>Состояние карточки товара</summary>
    [Column("active")]
    [JsonPropertyName("active")]
    [Comment("Состояние карточки товара")]
    public bool Active { get; set; }
}