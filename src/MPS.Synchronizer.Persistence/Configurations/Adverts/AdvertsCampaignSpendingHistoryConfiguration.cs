using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPS.Synchronizer.Domain.Entities.Adverts;
using MPS.Synchronizer.Persistence.Common;

namespace MPS.Synchronizer.Persistence.Configurations.Adverts;

public class AdvertsCampaignSpendingHistoryConfiguration : IEntityTypeConfiguration<AdvertsCampaignSpendingHistory>
{
    public void Configure(EntityTypeBuilder<AdvertsCampaignSpendingHistory> builder)
    {
        builder.ToTable("AdvertsCampaignSpendingHistory");

        builder.AutoGenerateIndexes();
    }
}