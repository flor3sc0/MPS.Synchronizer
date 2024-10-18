using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence.Common;

namespace MPS.Synchronizer.Persistence.Configurations.Statistics;

public class StatisticsIncomeConfiguration : IEntityTypeConfiguration<StatisticsIncome>
{
    public void Configure(EntityTypeBuilder<StatisticsIncome> builder)
    {
        builder.ToTable("StatisticsIncomes");

        builder.AutoGenerateIndexes();
    }
}