using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;

namespace Neptune.API.Hangfire
{
    public class PrecipitationZoneRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<PrecipitationZoneRefreshScheduledBackgroundJob>
    {
        public const string JobName = "Precipitation Zone Refresh";

        private readonly OCGISService _ocgisService;

        public int PersonID { get; set; }

        public PrecipitationZoneRefreshScheduledBackgroundJob(ILogger<PrecipitationZoneRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService, OCGISService ocgisService, int personID) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
            _ocgisService = ocgisService;
            PersonID = personID;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

        protected override void RunJobImplementation()
        {
            RunRefresh(DbContext);
        }

        public async void RunRefresh(NeptuneDbContext dbContext)
        {
            dbContext.Database.SetCommandTimeout(30000);
            await dbContext.PrecipitationZoneStagings.ExecuteDeleteAsync();
            var precipitationZoneFromEsris = await _ocgisService.RetrievePrecipitationZones();
            await SaveToStagingTable(precipitationZoneFromEsris);
            await MergeAndUpdateTreatmentBMPProperties(dbContext);
        }

        private async Task SaveToStagingTable(IEnumerable<OCGISService.PrecipitationZoneFromEsri> precipitationZoneFromEsris)
        {
            var precipitationZoneStagings = precipitationZoneFromEsris.Select(feature => new PrecipitationZoneStaging()
                {
                    PrecipitationZoneGeometry = feature.Geometry,
                    PrecipitationZoneKey = feature.PrecipitationZoneKey,
                    DesignStormwaterDepthInInches = feature.DesignStormwaterDepthInInches
                })
                .ToList();
            await DbContext.PrecipitationZoneStagings.AddRangeAsync(precipitationZoneStagings);
            await DbContext.SaveChangesAsync();
        }

        private static async Task MergeAndUpdateTreatmentBMPProperties(NeptuneDbContext dbContext)
        {
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pPrecipitationZoneUpdateFromStaging");
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pTreatmentBMPUpdatePrecipitationZone");
        }
    }
}