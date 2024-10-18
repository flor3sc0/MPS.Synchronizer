using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPS.Synchronizer.Domain.Entities.Statistics;
using MPS.Synchronizer.Persistence.Common;

namespace MPS.Synchronizer.Persistence.Configurations.Statistics;

public class StatisticsRealizationReportConfiguration : IEntityTypeConfiguration<StatisticsRealizationReport>
{
    public void Configure(EntityTypeBuilder<StatisticsRealizationReport> builder)
    {
        builder.ToTable("StatisticsRealizationReports");

        builder.AutoGenerateIndexes();
    }
}