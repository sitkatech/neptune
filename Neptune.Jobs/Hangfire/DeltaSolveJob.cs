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

    protected override async void RunJobImplementation()
    {
        var dirtyModelNodes = DbContext.DirtyModelNodes.ToList();
            
        await _nereidService.DeltaSolve(DbContext, dirtyModelNodes, true);
        await _nereidService.DeltaSolve(DbContext, dirtyModelNodes, false);

        DbContext.DirtyModelNodes.RemoveRange(dirtyModelNodes);
        DbContext.Database.SetCommandTimeout(600);
        await DbContext.SaveChangesAsync();
    }
}