using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;

namespace Neptune.Jobs.Hangfire
{
    public class HRURefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<HRURefreshScheduledBackgroundJob>
    {
        public const string JobName = "HRU Refresh";

        public HRURefreshScheduledBackgroundJob(ILogger<HRURefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneJobConfiguration, sitkaSmtpClientService)
        {
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Staging, RunEnvironment.Production };

        protected override void RunJobImplementation()
        {
            BackgroundJob.Enqueue<HRURefreshJob>(x => x.RunJob(45));
        }
    }
}