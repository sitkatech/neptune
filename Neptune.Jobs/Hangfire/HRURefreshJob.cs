﻿using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Jobs.EsriAsynchronousJob;
using Neptune.Jobs.Services;
using System.Diagnostics;

namespace Neptune.Jobs.Hangfire;

public class HRURefreshJob(
    IOptions<NeptuneJobConfiguration> configuration,
    ILogger<HRURefreshJob> logger,
    NeptuneDbContext dbContext,
    OCGISService ocgisService)
    : BlobStorageWritingJob<HRURefreshJob>(configuration, logger, dbContext)
{
    public const string LandUseStatisticsFileName = "LandUseStatistics.json";

    public async Task RunJob(int? maxTimeAllowed)
    {
        // this job assumes the LGUs are already correct but that for whatever reason, some are missing their HRUs

        // collect the load generating units that require updates,
        // group them by Model basin so requests to the HRU service are spatially bounded,
        // and batch them for processing 25 at a time so requests are small.

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var loadGeneratingUnitsToUpdateGroupedBySpatialGridUnit = dbContext.vLoadGeneratingUnitUpdateCandidates.ToList().GroupBy(x => x.SpatialGridUnitID);

        foreach (var group in loadGeneratingUnitsToUpdateGroupedBySpatialGridUnit.OrderByDescending(x => x.Count()))
        {
            try
            {
                var loadGeneratingUnitIDs = group.Select(x => x.PrimaryKey);
                await dbContext.LoadGeneratingUnits.Where(x => loadGeneratingUnitIDs
                        .Contains(x.LoadGeneratingUnitID))
                    .ExecuteUpdateAsync(x => x.SetProperty(y => y.DateHRURequested, DateTime.UtcNow));
                var hruResponseResult = await ProcessHRUsForLGUs(group, ocgisService, false, dbContext);

                var hruResponseFeatures = hruResponseResult.HRUResponseFeatures;
                if (!hruResponseFeatures.Any())
                {
                    var lguIDsWithProblems = hruResponseResult.LoadGeneratingUnitIDs;
                    await dbContext.LoadGeneratingUnits.Where(x =>
                            lguIDsWithProblems.Contains(x.LoadGeneratingUnitID))
                        .ExecuteUpdateAsync(x => 
                            x.SetProperty(y => y.IsEmptyResponseFromHRUService, true));

                    Logger.LogWarning($"No data for LGUs with these IDs: {string.Join(", ", lguIDsWithProblems)}");
                }

                await dbContext.LoadGeneratingUnits.Where(x => loadGeneratingUnitIDs
                        .Contains(x.LoadGeneratingUnitID))
                    .ExecuteUpdateAsync(x => x.SetProperty(y => y.HRULogID, hruResponseResult.HRULogID));

                //Now that HRULogs are linked with LGUs, delete the old logs
                await dbContext.Database.ExecuteSqlAsync($"EXEC dbo.pDeleteOldHRULogs");
                var hruCharacteristics = hruResponseFeatures.Select(x =>
                {
                    var hruCharacteristic = new HRUCharacteristic
                    {
                        LoadGeneratingUnitID = x.Attributes.QueryFeatureID
                    };
                    SetHRUCharacteristicProperties(x, hruCharacteristic);
                    return hruCharacteristic;
                });
                dbContext.HRUCharacteristics.AddRange(hruCharacteristics);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // this batch failed, but we don't want to give up the whole job.
                Logger.LogWarning(ex.Message);
            }

            if (maxTimeAllowed.HasValue && stopwatch.Elapsed.Minutes > maxTimeAllowed.Value)
            {
                break;
            }

            if (maxTimeAllowed.HasValue && stopwatch.Elapsed.Minutes > maxTimeAllowed.Value)
            {
                break;
            }
        }

        if (!maxTimeAllowed.HasValue)
        {
            // this implies a full hru refresh was run; let's go ahead and cache the hru results
            var list = dbContext.vPowerBILandUseStatistics.ToList();
            await SerializeAndUploadToBlobStorage(list, LandUseStatisticsFileName);
        }
        ExecuteNetworkSolveJobIfNeeded();

        stopwatch.Stop();
    }

    public static async Task<HRUResponseResult> ProcessHRUsForLGUs(IEnumerable<ILoadGeneratingUnit> loadGeneratingUnitsGroup, OCGISService ocgisService, bool isProject, NeptuneDbContext dbContext)
    {
        var loadGeneratingUnits = loadGeneratingUnitsGroup.ToList();
        var loadGeneratingUnitIDs = loadGeneratingUnits.Select(y => y.PrimaryKey);
        var hruRequestFeatures = HruRequestFeatureHelpers.GetHRURequestFeatures(
            loadGeneratingUnits.ToDictionary(x => x.PrimaryKey,
                x => x.Geometry), isProject);
        var hruResponseResult = await ocgisService.RetrieveHRUResponseFeatures(hruRequestFeatures.ToList(), dbContext);
        hruResponseResult.LoadGeneratingUnitIDs = loadGeneratingUnitIDs;
        return hruResponseResult;
    }

    private void ExecuteNetworkSolveJobIfNeeded()
    {
        var updatedRegionalSubbasins =
            dbContext.RegionalSubbasins.AsNoTracking().Where(x => x.LastUpdate != null).ToList();
        var lastRegionalSubbasinUpdateDate = updatedRegionalSubbasins.Any()
            ? updatedRegionalSubbasins.Max(x => x.LastUpdate.Value)
            : DateTime.MinValue;
        var updatedNereidResults = dbContext.NereidResults.AsNoTracking().Where(x => x.LastUpdate != null).ToList();
        var lastNereidResultUpdateDate = updatedNereidResults.Any()
            ? updatedNereidResults.Max(x => x.LastUpdate.Value)
            : DateTime.MinValue;

        if (lastRegionalSubbasinUpdateDate > lastNereidResultUpdateDate)
        {
            BackgroundJob.Enqueue<TotalNetworkSolveScheduledBackgroundJob>(x => x.RunJob(null));
        }
        else if (dbContext.DirtyModelNodes.AsNoTracking().Any())
        {
            BackgroundJob.Enqueue<DeltaSolveJob>(x => x.RunJob());
        }
    }

    public static void SetHRUCharacteristicProperties(HRUResponseFeature hruResponseFeature, IHRUCharacteristic hruCharacteristic)
    {
        //Can't generate an enum value with an empty name...so set to EMPTY here if necessary
        //But we also want to capture simply unrecognized codes (UNKNOWN), so EMPTY can't be the default
        hruResponseFeature.Attributes.ModelBasinLandUseDescription =
            String.IsNullOrWhiteSpace(hruResponseFeature.Attributes.ModelBasinLandUseDescription)
                ? HRUCharacteristicLandUseCode.EMPTY.HRUCharacteristicLandUseCodeName
                : hruResponseFeature.Attributes.ModelBasinLandUseDescription;
        hruResponseFeature.Attributes.BaselineLandUseDescription =
            String.IsNullOrWhiteSpace(hruResponseFeature.Attributes.BaselineLandUseDescription)
                ? HRUCharacteristicLandUseCode.EMPTY.HRUCharacteristicLandUseCodeName
                : hruResponseFeature.Attributes.BaselineLandUseDescription;
       
        var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(y =>
            y.HRUCharacteristicLandUseCodeName == hruResponseFeature.Attributes.ModelBasinLandUseDescription) ?? HRUCharacteristicLandUseCode.UNKNOWN;
        var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.SingleOrDefault(y =>
            y.HRUCharacteristicLandUseCodeName == hruResponseFeature.Attributes.BaselineLandUseDescription) ?? HRUCharacteristicLandUseCode.UNKNOWN;

        hruCharacteristic.HydrologicSoilGroup = hruResponseFeature.Attributes.HydrologicSoilGroup;
        hruCharacteristic.SlopePercentage = hruResponseFeature.Attributes.SlopePercentage.GetValueOrDefault();
        hruCharacteristic.ImperviousAcres = hruResponseFeature.Attributes.ImperviousAcres.GetValueOrDefault();
        hruCharacteristic.LastUpdated = DateTime.UtcNow;
        hruCharacteristic.Area = hruResponseFeature.Attributes.Acres.GetValueOrDefault();
        hruCharacteristic.HRUCharacteristicLandUseCodeID =
            hruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID;
        hruCharacteristic.BaselineImperviousAcres =
            hruResponseFeature.Attributes.BaselineImperviousAcres.GetValueOrDefault();
        hruCharacteristic.BaselineHRUCharacteristicLandUseCodeID =
            baselineHruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID;
    }
}