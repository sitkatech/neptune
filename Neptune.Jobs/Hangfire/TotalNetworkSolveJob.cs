using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Services;

namespace Neptune.Jobs.Hangfire;

public class TotalNetworkSolveJob
{
    private readonly ILogger<TotalNetworkSolveJob> _logger;
    private readonly NeptuneDbContext _dbContext;
    private readonly NereidService _nereidService;

    public TotalNetworkSolveJob(ILogger<TotalNetworkSolveJob> logger, NeptuneDbContext dbContext, NereidService nereidService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _nereidService = nereidService;
    }

    public async Task RunJob()
    {
        // clear out all dirty nodes since the whole network is being run.
        await _dbContext.DirtyModelNodes.ExecuteDeleteAsync();
        await _nereidService.TotalNetworkSolve(_dbContext, true);
        await _nereidService.TotalNetworkSolve(_dbContext, false);
    }
}