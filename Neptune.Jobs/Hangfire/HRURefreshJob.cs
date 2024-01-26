using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Neptune.EFModels.Entities;
using Neptune.Jobs.EsriAsynchronousJob;
using Neptune.Jobs.Services;
using System.Diagnostics;

namespace Neptune.Jobs.Hangfire;

public class HRURefreshJob
{
    private readonly ILogger<HRURefreshJob> _logger;
    private readonly NeptuneDbContext _dbContext;
    private readonly OCGISService _ocgisService;

    public HRURefreshJob(ILogger<HRURefreshJob> logger, NeptuneDbContext dbContext, OCGISService ocgisService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _ocgisService = ocgisService;
    }

    public async Task RunJob()
    {
        // this job assumes the LGUs are already correct but that for whatever reason, some are missing their HRUs

        // collect the load generating units that require updates,
        // group them by Model basin so requests to the HRU service are spatially bounded,
        // and batch them for processing 25 at a time so requests are small.

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var loadGeneratingUnitsToUpdateGroupedBySpatialGridUnit = _dbContext.vLoadGeneratingUnitUpdateCandidates.ToList().GroupBy(x => x.SpatialGridUnitID);

        foreach (var group in loadGeneratingUnitsToUpdateGroupedBySpatialGridUnit.OrderByDescending(x => x.Count()))
        {
            try
            {
                var loadGeneratingUnitIDs = group.Select(x => x.PrimaryKey);
                await _dbContext.LoadGeneratingUnits.Where(x => loadGeneratingUnitIDs
                        .Contains(x.LoadGeneratingUnitID))
                    .ExecuteUpdateAsync(x => x.SetProperty(y => y.DateHRURequested, DateTime.UtcNow));
                var hruResponseResult = await ProcessHRUsForLGUs(group, _ocgisService, false);

                var hruResponseFeatures = hruResponseResult.HRUResponseFeatures;
                if (!hruResponseFeatures.Any())
                {
                    var lguIDsWithProblems = hruResponseResult.LoadGeneratingUnitIDs;
                    await _dbContext.LoadGeneratingUnits.Where(x =>
                            lguIDsWithProblems.Contains(x.LoadGeneratingUnitID))
                        .ExecuteUpdateAsync(x => x.SetProperty(y => y.IsEmptyResponseFromHRUService, true));

                    _logger.LogWarning($"No data for LGUs with these IDs: {string.Join(", ", lguIDsWithProblems)}");
                }

                var hruCharacteristics = hruResponseFeatures.Select(x =>
                {
                    var hruCharacteristic = new HRUCharacteristic
                    {
                        LoadGeneratingUnitID = x.Attributes.QueryFeatureID
                    };
                    SetHRUCharacteristicProperties(x, hruCharacteristic);
                    return hruCharacteristic;
                });
                _dbContext.HRUCharacteristics.AddRange(hruCharacteristics);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // this batch failed, but we don't want to give up the whole job.
                _logger.LogWarning(ex.Message);
            }

            if (stopwatch.Elapsed.Minutes > 45)
            {
                break;
            }

            if (stopwatch.Elapsed.Minutes > 45)
            {
                break;
            }
        }

        ExecuteNetworkSolveJobIfNeeded();

        stopwatch.Stop();
    }

    public static async Task<HRUResponseResult> ProcessHRUsForLGUs(IEnumerable<ILoadGeneratingUnit> loadGeneratingUnitsGroup, OCGISService ocgisService, bool isProject)
    {
        var loadGeneratingUnits = loadGeneratingUnitsGroup.ToList();
        var loadGeneratingUnitIDs = loadGeneratingUnits.Select(y => y.PrimaryKey);
        var hruRequestFeatures = HruRequestFeatureHelpers.GetHRURequestFeatures(
            loadGeneratingUnits.ToDictionary(x => x.PrimaryKey,
                x => x.Geometry), isProject);
        var hruResponseFeatures = await ocgisService.RetrieveHRUResponseFeatures(hruRequestFeatures.ToList());
        return new HRUResponseResult(hruResponseFeatures, loadGeneratingUnitIDs);
    }

    private void ExecuteNetworkSolveJobIfNeeded()
    {
        var updatedRegionalSubbasins =
            _dbContext.RegionalSubbasins.AsNoTracking().Where(x => x.LastUpdate != null).ToList();
        var lastRegionalSubbasinUpdateDate = updatedRegionalSubbasins.Any()
            ? updatedRegionalSubbasins.Max(x => x.LastUpdate.Value)
            : DateTime.MinValue;
        var updatedNereidResults = _dbContext.NereidResults.AsNoTracking().Where(x => x.LastUpdate != null).ToList();
        var lastNereidResultUpdateDate = updatedNereidResults.Any()
            ? updatedNereidResults.Max(x => x.LastUpdate.Value)
            : DateTime.MinValue;

        if (lastRegionalSubbasinUpdateDate > lastNereidResultUpdateDate)
        {
            BackgroundJob.Enqueue<TotalNetworkSolveScheduledBackgroundJob>(x => x.RunJob(null));
        }
        else if (_dbContext.DirtyModelNodes.AsNoTracking().Any())
        {
            BackgroundJob.Enqueue<DeltaSolveJob>(x => x.RunJob());
        }
    }

    public static void SetHRUCharacteristicProperties(HRUResponseFeature hruResponseFeature, IHRUCharacteristic hruCharacteristic)
    {
        var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.Single(y =>
            y.HRUCharacteristicLandUseCodeName == hruResponseFeature.Attributes.ModelBasinLandUseDescription);
        var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.Single(y =>
            y.HRUCharacteristicLandUseCodeName == hruResponseFeature.Attributes.BaselineLandUseDescription);

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