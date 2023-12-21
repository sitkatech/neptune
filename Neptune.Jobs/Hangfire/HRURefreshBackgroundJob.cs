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

        protected override void RunJobImplementation()
        {
            HRURefreshImpl();
        }

        public void HRURefreshImpl()
        {
            // this job assumes the LGUs are already correct but that for whatever reason, some are missing their HRUs
            
            // collect the load generating units that require updates,
            // group them by Model basin so requests to the HRU service are spatially bounded,
            // and batch them for processing 25 at a time so requests are small.

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var lguUpdateCandidates = LoadGeneratingUnits.ListUpdateCandidates(DbContext);
            var loadGeneratingUnitsToUpdateGroupedByModelBasin = lguUpdateCandidates.GroupBy(x=> x.ModelBasinID);

            foreach (var group in loadGeneratingUnitsToUpdateGroupedByModelBasin)
            {
                var batches = group.Chunk(25);

                foreach (var batch in batches)
                {
                    try
                    {
                        var loadGeneratingUnits = batch.ToList();
                        var hruRequestFeatures = HruRequestFeatureHelpers.GetHRURequestFeatures(loadGeneratingUnits.ToDictionary(x => x.LoadGeneratingUnitID, x => x.LoadGeneratingUnitGeometry), false);
                        var hruResponseFeatures = _ocgisService.RetrieveHRUResponseFeatures(hruRequestFeatures.ToList()).Result;

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
                        DbContext.HRUCharacteristics.AddRange(hruCharacteristics);
                        DbContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        // this batch failed, but we don't want to give up the whole job.
                        Logger.LogWarning(ex.Message);
                    }

                    if (stopwatch.Elapsed.Minutes > 50)
                    {
                        break;
                    }
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
            var updatedRegionalSubbasins = DbContext.RegionalSubbasins.AsNoTracking().Where(x=> x.LastUpdate != null).ToList();
            var lastRegionalSubbasinUpdateDate = updatedRegionalSubbasins.Any() ? updatedRegionalSubbasins.Max(x=> x.LastUpdate.Value) : DateTime.MinValue;
            var updatedNereidResults = DbContext.NereidResults.AsNoTracking().Where(x=>x.LastUpdate != null).ToList();
            var lastNereidResultUpdateDate = updatedNereidResults.Any() ? updatedNereidResults.Max(x=> x.LastUpdate.Value) : DateTime.MinValue;

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
}