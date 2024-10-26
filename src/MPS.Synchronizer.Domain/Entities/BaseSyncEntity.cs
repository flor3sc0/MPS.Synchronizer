using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MPS.Synchronizer.Domain.Common;

namespace MPS.Synchronizer.Domain.Entities;

public abstract class BaseSyncEntity : BaseEntity
{
    /// <summary>
    /// Дата и время синхронизации записи через WB-Api
    /// </summary>
    [Comment("Дата и время синхронизации записи через WB-Api")]
    [SkipIndexGeneration]
    public DateTime SyncDateTime { get; set; }

    /// <summary>
    /// Юр. лицо которому принадлежит текущая запись, полученная через WB-Api
    /// </summary>
    [Comment("Юр. лицо которому принадлежит текущая запись, полученная через WB-Api")]
    [MaxLength(50)]
    [ForceIndexGeneration]
    public string LegalEntity { get; set; }
}