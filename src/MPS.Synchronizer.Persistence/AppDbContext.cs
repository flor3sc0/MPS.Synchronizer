using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MPS.Synchronizer.Domain.Entities;

namespace MPS.Synchronizer.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbContext DbContext => this;
        public DbConnection DbConnection => Database.GetDbConnection();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateEntities();

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public async Task MigrateAsync(CancellationToken cancellationToken = default)
        {
            await DbContext.Database.MigrateAsync(cancellationToken);
        }

        private void UpdateEntities()
        {
            foreach (var entry in ChangeTracker.Entries<BaseSyncEntity>())
            {
                if (entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified)
                    entry.Entity.SyncDateTime = DateTime.Now;
            }
        }
    }
}