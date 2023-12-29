using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Services;

namespace Neptune.Jobs.Hangfire;

public class DeltaSolveJob
{
    private readonly ILogger<DeltaSolveJob> _logger;
    private readonly NeptuneDbContext _dbContext;
    private readonly NereidService _nereidService;

    public DeltaSolveJob(ILogger<DeltaSolveJob> logger, NeptuneDbContext dbContext, NereidService nereidService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _nereidService = nereidService;
    }

    public async Task RunJob()
    {
        var dirtyModelNodes = _dbContext.DirtyModelNodes.ToList();

        await _nereidService.DeltaSolve(_dbContext, dirtyModelNodes, true);
        await _nereidService.DeltaSolve(_dbContext, dirtyModelNodes, false);

        _dbContext.DirtyModelNodes.RemoveRange(dirtyModelNodes);
        _dbContext.Database.SetCommandTimeout(600);
        await _dbContext.SaveChangesAsync();
    }
}