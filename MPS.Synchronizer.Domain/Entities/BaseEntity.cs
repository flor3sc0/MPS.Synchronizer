using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MPS.Synchronizer.Domain.Entities;

public abstract class BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    [Comment("Уникальный идентификатор записи")]
    [Key]
    public Guid Id { get; set; }
}