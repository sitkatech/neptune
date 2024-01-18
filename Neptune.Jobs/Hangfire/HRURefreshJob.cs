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

        var lguUpdateCandidates = LoadGeneratingUnits.ListUpdateCandidates(_dbContext);
        var loadGeneratingUnitsToUpdateGroupedByRegionalSubbasin = lguUpdateCandidates.GroupBy(x => x.RegionalSubbasinID);

        foreach (var group in loadGeneratingUnitsToUpdateGroupedByRegionalSubbasin)
        {
            try
            {
                var loadGeneratingUnits = group.ToList();
                var hruRequestFeatures = HruRequestFeatureHelpers.GetHRURequestFeatures(
                    loadGeneratingUnits.ToDictionary(x => x.LoadGeneratingUnitID,
                        x => x.LoadGeneratingUnitGeometry), false);
                var hruResponseFeatures =
                    await _ocgisService.RetrieveHRUResponseFeatures(hruRequestFeatures.ToList());

                if (!hruResponseFeatures.Any())
                {
                    foreach (var loadGeneratingUnit in loadGeneratingUnits)
                    {
                        loadGeneratingUnit.IsEmptyResponseFromHRUService = true;

                    }

                    _logger.LogWarning(
                        $"No data for LGUs with these IDs: {string.Join(", ", loadGeneratingUnits.Select(x => x.LoadGeneratingUnitID.ToString()))}");
                }

                var hruCharacteristics = hruResponseFeatures.Select(x =>
                {
                    var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.Single(y =>
                        string.Equals(y.HRUCharacteristicLandUseCodeName, x.Attributes.ModelBasinLandUseDescription,
                            StringComparison.CurrentCultureIgnoreCase));
                    var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.Single(y =>
                        string.Equals(y.HRUCharacteristicLandUseCodeName, x.Attributes.BaselineLandUseDescription,
                            StringComparison.CurrentCultureIgnoreCase));

                    var hruCharacteristic = new HRUCharacteristic
                    {
                        LoadGeneratingUnitID = x.Attributes.QueryFeatureID,
                        HydrologicSoilGroup = x.Attributes.HydrologicSoilGroup,
                        SlopePercentage = x.Attributes.SlopePercentage.GetValueOrDefault(),
                        HRUCharacteristicLandUseCodeID =
                            hruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID,
                        BaselineHRUCharacteristicLandUseCodeID =
                            baselineHruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID,
                        Area = x.Attributes.Acres.GetValueOrDefault(),

                        LastUpdated = DateTime.UtcNow,
                        ImperviousAcres = x.Attributes.ImperviousAcres.GetValueOrDefault(),
                        BaselineImperviousAcres = x.Attributes.BaselineImperviousAcres.GetValueOrDefault(),
                    };
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

            if (stopwatch.Elapsed.Minutes > 50)
            {
                break;
            }

            if (stopwatch.Elapsed.Minutes > 50)
            {
                break;
            }
        }

        ExecuteNetworkSolveJobIfNeeded();

        stopwatch.Stop();
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
}