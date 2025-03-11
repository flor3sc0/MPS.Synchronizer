using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MPS.Synchronizer.Domain.Entities.Adverts;

[Comment("Кампании")]
public class AdvertsCampaign : BaseSyncEntity
{
    /// <summary>
    /// ID кампании
    /// </summary>
    [Column("advertId")]
    [Comment("Идентификатор кампании")]
    public long AdvertId { get; set; }

    /// <summary>
    /// Тип кампании
    /// </summary>
    [Column("type")]
    [Comment("Тип кампании")]
    public int Type { get; set; }

    /// <summary>
    /// Статус кампании
    /// </summary>
    [Column("status")]
    [Comment("Статус кампании: 4 готова к запуску, 7 завершена, 8 отказался, 9 активна, 11 приостановлена")]
    public int Status { get; set; }

    /// <summary>
    /// Дата и время последнего изменения кампании
    /// </summary>
    [Column("changeTime")]
    [Comment("Дата и время последнего изменения кампании")]
    public DateTime ChangeTime { get; set; }
}