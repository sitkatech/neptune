using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.Services;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels.Entities;

namespace Neptune.Jobs.Hangfire
{
    public class TrashGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<TrashGeneratingUnitRefreshScheduledBackgroundJob>
    {
        private readonly QGISAPIService _qgisApiService;
        public const string JobName = "TGU Refresh";

        public TrashGeneratingUnitRefreshScheduledBackgroundJob(ILogger<TrashGeneratingUnitRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService, QGISAPIService qgisAPIService) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneJobConfiguration, sitkaSmtpClientService)
        {
            _qgisApiService = qgisAPIService;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

        protected override async void RunJobImplementation()
        {
            await _qgisApiService.GenerateTGUs(new GenerateTrashGeneratingUnitRequestDto());
        }
    }
}