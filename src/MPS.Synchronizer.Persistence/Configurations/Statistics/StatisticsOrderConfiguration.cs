using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence.Common;

namespace MPS.Synchronizer.Persistence.Configurations.Statistics;

public class StatisticsOrderConfiguration : IEntityTypeConfiguration<StatisticsOrder>
{
    public void Configure(EntityTypeBuilder<StatisticsOrder> builder)
    {
        builder.ToTable("StatisticsOrders");

        builder.AutoGenerateIndexes();
    }
}