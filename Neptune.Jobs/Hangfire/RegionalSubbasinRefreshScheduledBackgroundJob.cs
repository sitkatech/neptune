using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;

namespace Neptune.Jobs.Hangfire
{
    public class RegionalSubbasinRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<RegionalSubbasinRefreshScheduledBackgroundJob>
    {
        public const string JobName = "Regional Subbasin Refresh";

        public RegionalSubbasinRefreshScheduledBackgroundJob(ILogger<RegionalSubbasinRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneJobConfiguration, sitkaSmtpClientService)
        {
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Production };

        protected override void RunJobImplementation()
        {
            BackgroundJob.Enqueue<RegionalSubbasinRefreshJob>(x => x.RunJob());
        }
    }
}