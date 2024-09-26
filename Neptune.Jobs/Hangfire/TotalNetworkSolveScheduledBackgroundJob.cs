using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;

namespace Neptune.Jobs.Hangfire
{
    public class TotalNetworkSolveScheduledBackgroundJob : ScheduledBackgroundJobBase<TotalNetworkSolveScheduledBackgroundJob>
    {
        public const string JobName = "Nereid Total Network Solve";

        public TotalNetworkSolveScheduledBackgroundJob(ILogger<TotalNetworkSolveScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneJobConfiguration, sitkaSmtpClientService)
        {
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

        protected override void RunJobImplementation()
        {
            BackgroundJob.Enqueue<TotalNetworkSolveJob>(x => x.RunJob());
        }
    }
}