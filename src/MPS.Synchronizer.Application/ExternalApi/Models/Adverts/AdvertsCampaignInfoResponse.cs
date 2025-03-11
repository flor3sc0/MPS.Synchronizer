using System.Text.Json.Serialization;

namespace MPS.Synchronizer.Application.ExternalApi.Models.Adverts;

public class AdvertsCampaignInfoResponse
{
    /// <summary>ID кампании</summary>
    [JsonPropertyName("advertId")]
    public int AdvertId { get; set; }

    /// <summary>Время создания кампании</summary>
    [JsonPropertyName("createTime")]
    public DateTime CreateTime { get; set; }

    /// <summary>Дата последнего запуска кампании</summary>
    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }

    /// <summary>Время последнего изменения кампании</summary>
    [JsonPropertyName("changeTime")]
    public DateTime ChangeTime { get; set; }

    /// <summary>Дата завершения кампании</summary>
    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }

    /// <summary>Название кампании</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>Дневной бюджет, если не установлен, то 0</summary>
    [JsonPropertyName("dailyBudget")]
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
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>
    /// Тип кампании:
    /// 4 - кампания в каталоге (устаревший тип)
    /// 5 - кампания в карточке товара (устаревший тип)
    /// 6 - кампания в поиске (устаревший тип)
    /// 7 - кампания в рекомендациях на главной странице (устаревший тип)
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; set; }

    /// <summary>
    /// Модель оплаты:
    /// cpm — за показы
    /// cpo — за заказы
    /// </summary>
    [JsonPropertyName("paymentType")]
    public string PaymentType { get; set; } = string.Empty;

    /// <summary>
    /// Активность фиксированных фраз:
    /// false — не активны
    /// true — активны
    /// </summary>
    [JsonPropertyName("searchPluseState")]
    public bool SearchPluseState { get; set; }

    /// <summary>Параметры кампании</summary>
    [JsonPropertyName("params")]
    public List<CampaignParam> Params { get; set; } = new();

    /// <summary>Параметры кампании</summary>
    [JsonPropertyName("unitedParams")]
    public List<UnitedParam> UnitedParams { get; set; } = new();

    /// <summary>Ставки артикулов WB</summary>
    [JsonPropertyName("auction_multibids")]
    public List<AuctionMultibid> AuctionMultibids { get; set; } = new();

    /// <summary>Автоматические параметры кампании</summary>
    [JsonPropertyName("autoParams")]
    public AutoParams AutoParams { get; set; } = new();
}

public class CampaignParam
{
    /// <summary>Название предметной группы</summary>
    [JsonPropertyName("subjectName")]
    public string SubjectName { get; set; }

    /// <summary>Флаг активности предметной группы</summary>
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    /// <summary>Интервалы часов показа кампании</summary>
    [JsonPropertyName("intervals")]
    public List<Interval> Intervals { get; set; } = new();

    /// <summary>Текущая ставка</summary>
    [JsonPropertyName("price")]
    public int Price { get; set; }

    /// <summary>ID предметной группы</summary>
    [JsonPropertyName("subjectId")]
    public int SubjectId { get; set; }

    /// <summary>Массив карточек товаров кампании</summary>
    [JsonPropertyName("nms")]
    public List<Nm> Nms { get; set; } = new();
}

public class Interval
{
    /// <summary>Начало интервала</summary>
    [JsonPropertyName("begin")]
    public int Begin { get; set; }

    /// <summary>Конец интервала</summary>
    [JsonPropertyName("end")]
    public int End { get; set; }
}

public class Nm
{
    /// <summary>Числовой ID карточки товара WB (nmId)</summary>
    [JsonPropertyName("nm")]
    public int NmId { get; set; }

    /// <summary>Состояние карточки товара</summary>
    [JsonPropertyName("active")]
    public bool Active { get; set; }
}

public class UnitedParam
{
    /// <summary>Продвигаемый предмет</summary>
    [JsonPropertyName("subject")]
    public Subject Subject { get; set; } = new();

    /// <summary>Меню, связанные с кампанией</summary>
    [JsonPropertyName("menus")]
    public List<Menu> Menus { get; set; } = new();

    /// <summary>Артикулы WB</summary>
    [JsonPropertyName("nms")]
    public List<int> Nms { get; set; } = new();

    /// <summary>Ставка в поиске</summary>
    [JsonPropertyName("searchCPM")]
    public int SearchCpm { get; set; }

    /// <summary>Ставка в каталоге (при наличии)</summary>
    [JsonPropertyName("catalogCPM")]
    public int CatalogCpm { get; set; }
}

public class Subject
{
    /// <summary>ID предмета</summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>Название предмета</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class Menu
{
    /// <summary>ID меню</summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>Название меню</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public class AuctionMultibid
{
    /// <summary>Артикул WB</summary>
    [JsonPropertyName("nm")]
    public int Nm { get; set; }

    /// <summary>Ставка</summary>
    [JsonPropertyName("bid")]
    public int Bid { get; set; }
}

public class AutoParams
{
    /// <summary>Продвигаемый предмет</summary>
    [JsonPropertyName("subject")]
    public Subject Subject { get; set; } = new();

    /// <summary>Наборы предметов</summary>
    [JsonPropertyName("sets")]
    public List<Set> Sets { get; set; } = new();

    /// <summary>Артикулы WB</summary>
    [JsonPropertyName("nms")]
    public List<int> Nms { get; set; } = new();

    /// <summary>Активность опций</summary>
    [JsonPropertyName("active")]
    public ActiveOptions Active { get; set; } = new();

    /// <summary>Ставки артикулов WB</summary>
    [JsonPropertyName("nmCPM")]
    public List<NmCpm> NmCPM { get; set; } = new();
}

public class Set
{
    /// <summary>Название набора</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>ID набора</summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }
}

public class ActiveOptions
{
    /// <summary>Активность карусели</summary>
    [JsonPropertyName("carousel")]
    public bool Carousel { get; set; }

    /// <summary>Активность рекомендаций</summary>
    [JsonPropertyName("recom")]
    public bool Recom { get; set; }

    /// <summary>Активность бустера</summary>
    [JsonPropertyName("booster")]
    public bool Booster { get; set; }
}

public class NmCpm
{
    /// <summary>Артикул WB</summary>
    [JsonPropertyName("nm")]
    public int Nm { get; set; }

    /// <summary>Ставка</summary>
    [JsonPropertyName("cpm")]
    public int Cpm { get; set; }
}