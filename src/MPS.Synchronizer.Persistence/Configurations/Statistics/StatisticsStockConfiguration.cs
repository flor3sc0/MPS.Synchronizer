using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence.Common;

namespace MPS.Synchronizer.Persistence.Configurations.Statistics;

public class StatisticsStockConfiguration : IEntityTypeConfiguration<StatisticsStock>
{
    public void Configure(EntityTypeBuilder<StatisticsStock> builder)
    {
        builder.ToTable("StatisticsStocks");

        builder.AutoGenerateIndexes();
    }
}