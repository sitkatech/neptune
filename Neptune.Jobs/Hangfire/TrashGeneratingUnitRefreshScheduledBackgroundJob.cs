using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;

namespace Neptune.Jobs.Hangfire
{
    public class TrashGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<TrashGeneratingUnitRefreshScheduledBackgroundJob>
    {
        public const string JobName = "TGU Refresh";

        public TrashGeneratingUnitRefreshScheduledBackgroundJob(ILogger<TrashGeneratingUnitRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneJobConfiguration, sitkaSmtpClientService)
        {
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Staging, RunEnvironment.Production };

        protected override async void RunJobImplementation()
        {
            BackgroundJob.Enqueue<TrashGeneratingUnitRefreshJob>(x => x.RunJob());
        }
    }
}