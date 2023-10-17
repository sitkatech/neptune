using System.Diagnostics;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;
using Neptune.Jobs.EsriAsynchronousJob;
using Neptune.Jobs.Services;
using Exception = System.Exception;

namespace Neptune.Jobs.Hangfire
{
    public class HRURefreshBackgroundJob : ScheduledBackgroundJobBase<HRURefreshBackgroundJob>
    {
        private readonly OCGISService _ocgisService;
        public const string JobName = "HRU Refresh";

        public HRURefreshBackgroundJob(ILogger<HRURefreshBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService, OCGISService ocgisService) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneJobConfiguration, sitkaSmtpClientService)
        {
            _ocgisService = ocgisService;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

        protected override async void RunJobImplementation()
        {
            await HRURefreshImpl();
        }

        private async Task HRURefreshImpl()
        {
            // this job assumes the LGUs are already correct but that for whatever reason, some are missing their HRUs
            
            // collect the load generating units that require updates,
            // group them by Model basin so requests to the HRU service are spatially bounded,
            // and batch them for processing 25 at a time so requests are small.

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var loadGeneratingUnitsToUpdate = GetLoadGeneratingUnitsToUpdate(DbContext).ToList();

            if (!loadGeneratingUnitsToUpdate.Any())
            {
                var lgusWithEmptyResponseCount = DbContext.LoadGeneratingUnits.Count(x=> x.IsEmptyResponseFromHRUService == true);
                if (lgusWithEmptyResponseCount > 100)
                {
                    foreach (var loadGeneratingUnit in DbContext.LoadGeneratingUnits.Where(x => x.IsEmptyResponseFromHRUService == true))
                    {
                        loadGeneratingUnit.IsEmptyResponseFromHRUService = false;
                    }
                    await DbContext.SaveChangesAsync();
                    loadGeneratingUnitsToUpdate = GetLoadGeneratingUnitsToUpdate(DbContext).ToList();
                }
            }

            var loadGeneratingUnitsToUpdateGroupedByModelBasin = loadGeneratingUnitsToUpdate.GroupBy(x=> x.ModelBasinID);

            foreach (var group in loadGeneratingUnitsToUpdateGroupedByModelBasin)
            {
                var batches = group.Chunk(25);

                foreach (var batch in batches)
                {
                    try
                    {
                        var loadGeneratingUnits = batch.ToList();
                        var hruRequestFeatures = HruRequestFeatureHelpers.GetHRURequestFeatures(loadGeneratingUnits.ToDictionary(x => x.LoadGeneratingUnitID, x => x.LoadGeneratingUnitGeometry), false);
                        var hruResponseFeatures = await _ocgisService.RetrieveHRUResponseFeatures(hruRequestFeatures.ToList());

                        if (!hruResponseFeatures.Any())
                        {
                            foreach (var loadGeneratingUnit in loadGeneratingUnits)
                            {
                                loadGeneratingUnit.IsEmptyResponseFromHRUService = true;

                            }
                            Logger.LogWarning($"No data for LGUs with these IDs: {string.Join(", ", loadGeneratingUnits.Select(x => x.LoadGeneratingUnitID.ToString()))}");
                        }

                        var hruCharacteristics = hruResponseFeatures.Select(x =>
                        {
                            var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.Single(y => string.Equals(y.HRUCharacteristicLandUseCodeName, x.Attributes.ModelBasinLandUseDescription, StringComparison.CurrentCultureIgnoreCase));
                            var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.Single(y => string.Equals(y.HRUCharacteristicLandUseCodeName, x.Attributes.BaselineLandUseDescription, StringComparison.CurrentCultureIgnoreCase));

                            var hruCharacteristic = new HRUCharacteristic
                            {
                                LoadGeneratingUnitID = x.Attributes.QueryFeatureID,
                                HydrologicSoilGroup = x.Attributes.HydrologicSoilGroup,
                                SlopePercentage = x.Attributes.SlopePercentage.GetValueOrDefault(),
                                ImperviousAcres = x.Attributes.ImperviousAcres.GetValueOrDefault(),
                                LastUpdated = DateTime.Now,
                                Area = x.Attributes.Acres.GetValueOrDefault(),
                                HRUCharacteristicLandUseCodeID = hruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID,
                                BaselineImperviousAcres = x.Attributes.BaselineImperviousAcres.GetValueOrDefault(),
                                BaselineHRUCharacteristicLandUseCodeID = baselineHruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID
                            };
                            return hruCharacteristic;
                        });
                        await DbContext.HRUCharacteristics.AddRangeAsync(hruCharacteristics);
                        await DbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        // this batch failed, but we don't want to give up the whole job.
                        Logger.LogWarning(ex.Message);
                    }

                    if (stopwatch.Elapsed.Minutes > 20)
                    {
                        break;
                    }
                }

                if (stopwatch.Elapsed.Minutes > 20)
                {
                    break;
                }
            }

            ExecuteModelIfNeeded(loadGeneratingUnitsToUpdate.Any());

            stopwatch.Stop();
        }

        private void ExecuteModelIfNeeded(bool wereAnyLoadGeneratingUnitsToUpdate)
        {
            var updatedRegionalSubbasins = DbContext.RegionalSubbasins.Where(x=> x.LastUpdate != null).ToList();
            var lastRegionalSubbasinUpdateDate = updatedRegionalSubbasins.Any() ? updatedRegionalSubbasins.Max(x=> x.LastUpdate.Value) : DateTime.MinValue;
            var updatedNereidResults = DbContext.NereidResults.Where(x=>x.LastUpdate != null).ToList();
            var lastNereidResultUpdateDate = updatedNereidResults.Any() ? updatedNereidResults.Max(x=> x.LastUpdate.Value) : DateTime.MinValue;
            
            if (wereAnyLoadGeneratingUnitsToUpdate)
            {
                // if there was any work done, check if all the HRUs are populated and if so blast off with a new solve.
                DbContext.LoadGeneratingUnits.Load();

                // don't die if it takes longer than 30 seconds for this next query to come back
                DbContext.Database.SetCommandTimeout(600);
                var loadGeneratingUnitsMissingHrus = GetLoadGeneratingUnitsToUpdate(DbContext).Any();

                if (!loadGeneratingUnitsMissingHrus)
                {
                    if (lastRegionalSubbasinUpdateDate > lastNereidResultUpdateDate)
                    {
                        BackgroundJob.Enqueue<TotalNetworkSolveJob>(x => x.RunJob(null));
                    }
                    else if(DbContext.DirtyModelNodes.Any())
                    {
                        BackgroundJob.Enqueue<DeltaSolveJob>(x => x.RunJob(null));
                    }
                }
            }
            else
            {
                // if the job woke up and went immediately to sleep, then all HRUs are populated.
                if (lastRegionalSubbasinUpdateDate > lastNereidResultUpdateDate)
                {
                    BackgroundJob.Enqueue<TotalNetworkSolveJob>(x => x.RunJob(null));
                }
                else if (DbContext.DirtyModelNodes.AsNoTracking().Any())
                {
                    BackgroundJob.Enqueue<DeltaSolveJob>(x => x.RunJob(null));
                }
            }
        }

        private static IQueryable<LoadGeneratingUnit> GetLoadGeneratingUnitsToUpdate(NeptuneDbContext dbContext)
        {
            return dbContext.LoadGeneratingUnits.Include(x => x.RegionalSubbasin)
                .Include(x => x.HRUCharacteristics)
                .Where(x =>
                    x.RegionalSubbasin != null &&
                    x.ModelBasinID == x.RegionalSubbasin.ModelBasinID.Value &&
                    !(x.HRUCharacteristics.Any() || x.RegionalSubbasinID == null ||
                      x.IsEmptyResponseFromHRUService == true) && x.LoadGeneratingUnitGeometry.Area >= 10);
        }
    }
}