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
    public class DelineationDiscrepancyCheckerBackgroundJob : ScheduledBackgroundJobBase<DelineationDiscrepancyCheckerBackgroundJob>
    {
        public const string JobName = "Delineation Discrepancy Checker";

        public DelineationDiscrepancyCheckerBackgroundJob(ILogger<DelineationDiscrepancyCheckerBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

        protected override void RunJobImplementation()
        {
            DbContext.Database.SetCommandTimeout(30000);
            DbContext.Database.ExecuteSqlRaw("EXEC dbo.pDelineationMarkThoseThatHaveDiscrepancies");
        }
    }
}