using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Services;

namespace Neptune.Jobs.Hangfire
{
    public class RegionalSubbasinRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<RegionalSubbasinRefreshScheduledBackgroundJob>
    {
        public const string JobName = "Regional Subbasin Refresh";

        private readonly OCGISService _ocgisService;
        public bool QueueLGURefresh { get; set; }

        public RegionalSubbasinRefreshScheduledBackgroundJob(ILogger<RegionalSubbasinRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService, OCGISService ocgisService, bool queueLGURefresh) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneJobConfiguration, sitkaSmtpClientService)
        {
            _ocgisService = ocgisService;
            QueueLGURefresh = queueLGURefresh;
        }

        protected override void RunJobImplementation()
        {
            _ocgisService.RefreshSubbasins();
            UpdateLoadGeneratingUnits();
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };
    
        private static void UpdateLoadGeneratingUnits()
        {
            BackgroundJob.Enqueue<LoadGeneratingUnitRefreshJob>(x => x.RunJob(null, true));
        }
    }
}