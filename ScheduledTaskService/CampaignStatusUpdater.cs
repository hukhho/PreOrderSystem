using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PreOrderPlatform.Entity.Repositories.CampaignRepositories;

namespace ScheduledTaskService;

public class CampaignStatusUpdater : BackgroundService
{
    private readonly ILogger<CampaignStatusUpdater> _logger;
    private readonly IServiceProvider _serviceProvider;

    public CampaignStatusUpdater(ILogger<CampaignStatusUpdater> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("CampaignStatusUpdater running at: {time}", DateTimeOffset.Now);

            using (var scope = _serviceProvider.CreateScope())
            {
                var campaignRepository = scope.ServiceProvider.GetRequiredService<ICampaignRepository>();

                var campaignsToUpdate = await campaignRepository.GetAllAsync();

                foreach (var campaign in campaignsToUpdate)
                {
                    // Your logic to update campaign status goes here.
                }
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}