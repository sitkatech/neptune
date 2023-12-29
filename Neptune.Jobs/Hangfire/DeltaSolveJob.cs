using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Services;

namespace Neptune.Jobs.Hangfire;

public class DeltaSolveJob : ScheduledBackgroundJobBase<DeltaSolveJob>
{
    public const string JobName = "Nereid Delta Solve";

    private readonly NereidService _nereidService;

    public DeltaSolveJob(ILogger<DeltaSolveJob> logger,
        IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
        IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService, NereidService nereidService) : base(JobName, logger, webHostEnvironment,
        neptuneDbContext, neptuneJobConfiguration, sitkaSmtpClientService)
    {
        _nereidService = nereidService;
    }

    public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

    protected override void RunJobImplementation()
    {
        var dirtyModelNodes = DbContext.DirtyModelNodes.ToList();

        var networkSolveResultBaseline = _nereidService.DeltaSolve(DbContext, dirtyModelNodes, true).Result;
        var networkSolveResult = _nereidService.DeltaSolve(DbContext, dirtyModelNodes, false).Result;

        DbContext.DirtyModelNodes.RemoveRange(dirtyModelNodes);
        DbContext.Database.SetCommandTimeout(600);
        DbContext.SaveChanges();
    }
}