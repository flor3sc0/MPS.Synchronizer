using System.Text.Json.Serialization;

namespace MPS.Synchronizer.Application.ExternalApi.Models.Adverts;


public class AdvertsCampaignsResponse
{
    /// <summary>
    /// Данные по кампаниям
    /// </summary>
    [JsonPropertyName("adverts")]
    public List<AdvertInfo> Adverts { get; set; }

    /// <summary>
    /// Общее количество кампаний всех статусов и типов
    /// </summary>
    [JsonPropertyName("all")]
    public int All { get; set; }
}

public class AdvertInfo
{
    /// <summary>
    /// Тип кампании
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }

    /// <summary>
    /// Статус кампании
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>
    /// Количество кампаний
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }

    /// <summary>
    /// Список кампаний
    /// </summary>
    [JsonPropertyName("advert_list")]
    public List<Advert> AdvertList { get; set; } = new();
}

public class Advert
{
    /// <summary>
    /// ID кампании
    /// </summary>
    [JsonPropertyName("advertId")]
    public long AdvertId { get; set; }

    /// <summary>
    /// Дата и время последнего изменения кампании
    /// </summary>
    [JsonPropertyName("changeTime")]
    public DateTime ChangeTime { get; set; }
}