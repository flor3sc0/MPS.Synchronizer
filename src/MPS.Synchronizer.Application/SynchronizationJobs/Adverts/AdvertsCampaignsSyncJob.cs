using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using MPS.Synchronizer.Application.CommonModels;
using MPS.Synchronizer.Application.Extensions;
using MPS.Synchronizer.Application.ExternalApi.Interfaces;
using MPS.Synchronizer.Domain.Entities.Adverts;
using MPS.Synchronizer.Persistence;
using Serilog;

namespace MPS.Synchronizer.Application.SynchronizationJobs.Adverts;

public class AdvertsCampaignsSyncJob(IWbAdvertsApi apiService, AppDbContext appDbContext, LegalEntityOptions options) : IInvocable
{
    public async Task Invoke()
    {
        Log.Information($"Invoke {GetType().Name} for '{options.Name}'");

        var items = await apiService.GetCampaignsAsync(options.Token);

        var campaigns =
            (from item in items.Adverts
             from advert in item.AdvertList
             select
                 new AdvertsCampaign
                 {
                     AdvertId = advert.AdvertId,
                     ChangeTime = advert.ChangeTime,
                     Status = item.Status,
                     Type = item.Type
                 })
            .ToList();

        await appDbContext.Set<AdvertsCampaign>()
            .Where(x => x.LegalEntity == options.Name)
            .ExecuteDeleteAsync();

        campaigns.EnrichByLegalEntity(options.Name);
        await appDbContext.Set<AdvertsCampaign>().AddRangeAsync(campaigns);
        await appDbContext.SaveChangesAsync();

        Log.Information($"Invoked {GetType().Name} for '{options.Name}' with {campaigns.Count} items\n");
    }
}