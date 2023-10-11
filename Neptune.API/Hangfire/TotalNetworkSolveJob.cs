using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;

namespace Neptune.API.Hangfire
{
    public class TotalNetworkSolveJob : ScheduledBackgroundJobBase<TotalNetworkSolveJob>
    {
        public const string JobName = "Nereid Total Network Solve";

        private readonly NereidService _nereidService;

        public TotalNetworkSolveJob(ILogger<TotalNetworkSolveJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService, NereidService nereidService) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
            _nereidService = nereidService;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Staging, RunEnvironment.Production };

        protected override async void RunJobImplementation()
        {
            // clear out all dirty nodes since the whole network is being run.
            await DbContext.DirtyModelNodes.ExecuteDeleteAsync();
            await _nereidService.TotalNetworkSolve(DbContext, true);
            await _nereidService.TotalNetworkSolve(DbContext, false);

        }
    }
}