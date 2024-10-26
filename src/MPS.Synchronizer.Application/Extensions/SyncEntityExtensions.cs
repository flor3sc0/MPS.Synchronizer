using MPS.Synchronizer.Domain.Entities;

namespace MPS.Synchronizer.Application.Extensions;

public static class SyncEntityExtensions
{
    public static void EnrichByLegalEntity<T>(this List<T> items, string legalEntity) where T : BaseSyncEntity
    {
        foreach (var item in items) item.LegalEntity = legalEntity;
    }
}