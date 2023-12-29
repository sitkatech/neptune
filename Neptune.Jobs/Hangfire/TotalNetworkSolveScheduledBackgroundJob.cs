using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Services;

namespace Neptune.Jobs.Hangfire
{
    public class TotalNetworkSolveScheduledBackgroundJob : ScheduledBackgroundJobBase<TotalNetworkSolveScheduledBackgroundJob>
    {
        public const string JobName = "Nereid Total Network Solve";

        private readonly NereidService _nereidService;

        public TotalNetworkSolveScheduledBackgroundJob(ILogger<TotalNetworkSolveScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService, NereidService nereidService) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneJobConfiguration, sitkaSmtpClientService)
        {
            _nereidService = nereidService;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Staging, RunEnvironment.Production };

        protected override void RunJobImplementation()
        {
            // clear out all dirty nodes since the whole network is being run.
            DbContext.DirtyModelNodes.ExecuteDelete();
            _nereidService.TotalNetworkSolve(DbContext, true);
            _nereidService.TotalNetworkSolve(DbContext, false);

        }
    }
}