using Hangfire;
using Microsoft.Extensions.Logging;
using Neptune.Common.Services;
using Neptune.Common.Services.GDAL;

namespace Neptune.Jobs.Hangfire
{
    public class LoadGeneratingUnitRefreshJob
    {
        private readonly ILogger<LoadGeneratingUnitRefreshJob> _logger;
        private readonly QGISAPIService _qgisApiService;

        public LoadGeneratingUnitRefreshJob(ILogger<LoadGeneratingUnitRefreshJob> logger, QGISAPIService qgisAPIService)
        {
            _logger = logger;
            _qgisApiService = qgisAPIService;
        }

        public async Task RunJob(int? loadGeneratingUnitRefreshAreaID, bool runHRURefreshJob)
        {
            await _qgisApiService.GenerateLGUs(new GenerateLoadGeneratingUnitRequestDto()
                { LoadGeneratingUnitRefreshAreaID = loadGeneratingUnitRefreshAreaID });
            if (runHRURefreshJob)
            {
                BackgroundJob.Enqueue<HRURefreshBackgroundJob>(x => x.RunJob(null));
            }
        }
    }
}