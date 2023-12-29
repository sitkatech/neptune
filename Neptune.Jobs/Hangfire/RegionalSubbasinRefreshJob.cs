using Hangfire;
using Microsoft.Extensions.Logging;
using Neptune.Jobs.Services;

namespace Neptune.Jobs.Hangfire;

public class RegionalSubbasinRefreshJob
{
    private readonly ILogger<RegionalSubbasinRefreshJob> _logger;
    private readonly OCGISService _ocgisService;

    public RegionalSubbasinRefreshJob(ILogger<RegionalSubbasinRefreshJob> logger, OCGISService ocgisService)
    {
        _logger = logger;
        _ocgisService = ocgisService;
    }

    public async Task RunJob()
    {
        await _ocgisService.RefreshSubbasins();
        BackgroundJob.Enqueue<LoadGeneratingUnitRefreshJob>(x => x.RunJob(null, true));
    }
}