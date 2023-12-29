using Microsoft.Extensions.Logging;
using Neptune.Common.Services;
using Neptune.Common.Services.GDAL;

namespace Neptune.Jobs.Hangfire;

public class TrashGeneratingUnitRefreshJob
{
    private readonly ILogger<TrashGeneratingUnitRefreshJob> _logger;
    private readonly QGISAPIService _qgisApiService;

    public TrashGeneratingUnitRefreshJob(ILogger<TrashGeneratingUnitRefreshJob> logger, QGISAPIService qgisAPIService)
    {
        _logger = logger;
        _qgisApiService = qgisAPIService;
    }

    public async Task RunJob()
    {
        await _qgisApiService.GenerateTGUs(new GenerateTrashGeneratingUnitRequestDto());
    }
}