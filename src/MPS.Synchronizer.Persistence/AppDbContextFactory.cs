using Microsoft.EntityFrameworkCore;

namespace MPS.Synchronizer.Persistence
{
    public class AppDbContextFactory : DesignTimeDbContextFactoryBase<AppDbContext>
    {
        protected override AppDbContext CreateNewInstance(DbContextOptions<AppDbContext> options)
            => new AppDbContext(options);
    }
}
