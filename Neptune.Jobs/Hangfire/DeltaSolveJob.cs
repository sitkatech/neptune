using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Services;
using System.Text.Json.Nodes;

namespace Neptune.Jobs.Hangfire;

public class DeltaSolveJob(
    IOptions<NeptuneJobConfiguration> configuration,
    ILogger<DeltaSolveJob> logger,
    NeptuneDbContext dbContext,
    NereidService nereidService)
    : BlobStorageWritingJob<DeltaSolveJob>(configuration, logger, dbContext)
{
    public async Task RunJob()
    {
        var dirtyModelNodes = DbContext.DirtyModelNodes.ToList();

        await nereidService.DeltaSolve(DbContext, dirtyModelNodes, true);
        await nereidService.DeltaSolve(DbContext, dirtyModelNodes, false);

        DbContext.DirtyModelNodes.RemoveRange(dirtyModelNodes);
        DbContext.Database.SetCommandTimeout(600);
        await DbContext.SaveChangesAsync();

        var allBaselineNereidResults = DbContext.NereidResults.Where(x => x.IsBaselineCondition);
        var allNereidResults = DbContext.NereidResults.Where(x => !x.IsBaselineCondition);
        await CreateNereidResultsAsJsonFileAndPostToBlobStorage(allBaselineNereidResults, TotalNetworkSolveJob.BaselineModelResultsFileName);
        await CreateNereidResultsAsJsonFileAndPostToBlobStorage(allNereidResults, TotalNetworkSolveJob.ModelResultsFileName);

        var landUseStatistics = DbContext.vPowerBILandUseStatistics.ToList();
        await SerializeAndUploadToBlobStorage(landUseStatistics, HRURefreshJob.LandUseStatisticsFileName);
    }

    private async Task CreateNereidResultsAsJsonFileAndPostToBlobStorage(IEnumerable<NereidResult> nereidResults, string blobFilename)
    {
        var list = nereidResults.Select(x =>
        {
            var jsonObject = GeoJsonSerializer.Deserialize<JsonObject>(x.FullResponse);
            jsonObject["TreatmentBMPID"] = x.TreatmentBMPID;
            jsonObject["WaterQualityManagementPlanID"] = x.WaterQualityManagementPlanID;
            jsonObject["DelineationID"] = x.DelineationID;
            jsonObject["RegionalSubbasinID"] = x.RegionalSubbasinID;
            return jsonObject;
        }).ToList();

        await SerializeAndUploadToBlobStorage(list, blobFilename);
    }
}