using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence.Common;

namespace MPS.Synchronizer.Persistence.Configurations.Statistics;

public class StatisticsSaleConfiguration : IEntityTypeConfiguration<StatisticsSale>
{
    public void Configure(EntityTypeBuilder<StatisticsSale> builder)
    {
        builder.ToTable("StatisticsSales");

        builder.AutoGenerateIndexes();
    }
}