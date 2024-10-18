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
}