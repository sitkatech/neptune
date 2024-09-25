﻿using Microsoft.EntityFrameworkCore;
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
        // clear out all the nereid results since we are rerunning it for the whole network
        await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDeleteNereidResults");
        // we need to set the NereidLog to null

        await _dbContext.TreatmentBMPNereidLogs.ExecuteUpdateAsync(
            x => x
                .SetProperty(y => y.LastRequestDate, (DateTime?) null)
                .SetProperty(y => y.NereidRequest, (string?) null)
                .SetProperty(y => y.NereidResponse, (string?) null)
        );

        await _dbContext.WaterQualityManagementPlanNereidLogs.ExecuteUpdateAsync(
            x => x
                .SetProperty(y => y.LastRequestDate, (DateTime?) null)
                .SetProperty(y => y.NereidRequest, (string?) null)
                .SetProperty(y => y.NereidResponse, (string?) null)
        );

        await _nereidService.TotalNetworkSolve(_dbContext, true);
        await _nereidService.TotalNetworkSolve(_dbContext, false);
    }
}