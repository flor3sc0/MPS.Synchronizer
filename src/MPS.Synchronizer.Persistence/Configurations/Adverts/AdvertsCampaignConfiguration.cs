using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MPS.Synchronizer.Domain.Entities.Adverts;
using MPS.Synchronizer.Persistence.Common;

namespace MPS.Synchronizer.Persistence.Configurations.Adverts;

public class AdvertsCampaignConfiguration : IEntityTypeConfiguration<AdvertsCampaign>
{
    public void Configure(EntityTypeBuilder<AdvertsCampaign> builder)
    {
        builder.ToTable("AdvertsCampaigns");

        builder.AutoGenerateIndexes();
    }
}