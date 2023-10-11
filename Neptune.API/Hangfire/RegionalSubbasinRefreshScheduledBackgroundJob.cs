using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Services;
using Neptune.EFModels.Entities;

namespace Neptune.API.Hangfire
{
    public class RegionalSubbasinRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<RegionalSubbasinRefreshScheduledBackgroundJob>
    {
        public const string JobName = "Regional Subbasin Refresh";

        private readonly OCGISService _ocgisService;
        public bool QueueLGURefresh { get; set; }

        public RegionalSubbasinRefreshScheduledBackgroundJob(ILogger<RegionalSubbasinRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService, OCGISService ocgisService, bool queueLGURefresh) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
            _ocgisService = ocgisService;
            QueueLGURefresh = queueLGURefresh;
        }

        protected override async void RunJobImplementation()
        {
            await RunRefresh(DbContext);
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };
    
        public async Task RunRefresh(NeptuneDbContext dbContext)
        {
            dbContext.Database.SetCommandTimeout(30000);
            await dbContext.RegionalSubbasinStagings.ExecuteDeleteAsync();
            var regionalSubbasinFromEsris = await _ocgisService.RetrieveRegionalSubbasins();
            await SaveToStagingTable(regionalSubbasinFromEsris);
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDeleteLoadGeneratingUnitsPriorToTotalRefresh");
            await MergeAndProjectTo4326(dbContext);
            await RefreshCentralizedDelineations(dbContext);
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pDelineationMarkThoseThatHaveDiscrepancies");

            UpdateLoadGeneratingUnits();
        }

        private static void UpdateLoadGeneratingUnits()
        {
            // Instead, just queue a total LGU update
            BackgroundJob.Enqueue<LoadGeneratingUnitRefreshScheduledBackgroundJob>(x => x.RunJob(null));

            // And follow it up with an HRU update
            BackgroundJob.Enqueue<HRURefreshBackgroundJob>(x => x.RunJob(null));
        }

        private async Task SaveToStagingTable(IEnumerable<OCGISService.RegionalSubbasinFromEsri> regionalSubbasinFromEsris)
        {
            var regionalSubbasinStagings = regionalSubbasinFromEsris.Select(feature => new RegionalSubbasinStaging()
                {
                    CatchmentGeometry = feature.Geometry,
                    Watershed = feature.Watershed,
                    OCSurveyCatchmentID = feature.OCSurveyCatchmentID,
                    OCSurveyDownstreamCatchmentID = feature.OCSurveyDownstreamCatchmentID,
                    DrainID = feature.DrainID
                })
                .ToList();
            await DbContext.RegionalSubbasinStagings.AddRangeAsync(regionalSubbasinStagings);
            await DbContext.SaveChangesAsync();
        }

        private static async Task RefreshCentralizedDelineations(NeptuneDbContext dbContext)
        {
            foreach (var delineation in dbContext.Delineations.Where(x => x.DelineationTypeID == DelineationType.Centralized.DelineationTypeID))
            {
                var centralizedDelineationGeometry2771 = delineation.TreatmentBMP.GetCentralizedDelineationGeometry2771(dbContext);
                var centralizedDelineationGeometry4326 = delineation.TreatmentBMP.GetCentralizedDelineationGeometry4326(dbContext);

                delineation.DelineationGeometry = centralizedDelineationGeometry2771;
                delineation.DelineationGeometry4326 = centralizedDelineationGeometry4326;
                delineation.DateLastModified = DateTime.Now;
            }

            await dbContext.SaveChangesAsync();
        }

        private static async Task MergeAndProjectTo4326(NeptuneDbContext dbContext)
        {
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pUpdateRegionalSubbasinLiveFromStaging");
            await dbContext.RegionalSubbasins.LoadAsync();
            await dbContext.Watersheds.LoadAsync();
            foreach (var regionalSubbasin in dbContext.RegionalSubbasins)
            {
                regionalSubbasin.CatchmentGeometry4326 = regionalSubbasin.CatchmentGeometry.ProjectTo4326();
            }

            // Watershed table is made up from the dissolves/ aggregation of the Regional Subbasins feature layer, so we need to update it when Regional Subbasins are updated
            foreach (var watershed in dbContext.Watersheds)
            {
                watershed.WatershedGeometry4326 = watershed.WatershedGeometry.ProjectTo4326();
            }
            await dbContext.SaveChangesAsync();
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pTreatmentBMPUpdateWatershed");
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pUpdateRegionalSubbasinIntersectionCache");
        }
    }
}