using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.Common.Email;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;

namespace Neptune.API.Hangfire
{
    public class OCTAPrioritizationRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<OCTAPrioritizationRefreshScheduledBackgroundJob>
    {
        private readonly OCGISService _ocgisService;
        public const string JobName = "OCTA Prioritization Refresh";

        public int PersonID { get; set; }

        public OCTAPrioritizationRefreshScheduledBackgroundJob(ILogger<OCTAPrioritizationRefreshScheduledBackgroundJob> logger,
            IWebHostEnvironment webHostEnvironment, NeptuneDbContext neptuneDbContext,
            IOptions<NeptuneConfiguration> neptuneConfiguration, SitkaSmtpClientService sitkaSmtpClientService, OCGISService ocgisService, int personID) : base(JobName, logger, webHostEnvironment,
            neptuneDbContext, neptuneConfiguration, sitkaSmtpClientService)
        {
            _ocgisService = ocgisService;
            PersonID = personID;
        }

        public override List<RunEnvironment> RunEnvironments => new() { RunEnvironment.Development, RunEnvironment.Staging, RunEnvironment.Production };

        protected override async void RunJobImplementation()
        {
            await RunRefresh(DbContext);
        }

        public async Task RunRefresh(NeptuneDbContext dbContext)
        {
            dbContext.Database.SetCommandTimeout(30000);
            await dbContext.OCTAPrioritizationStagings.ExecuteDeleteAsync();
            var octaPrioritizationFromEsris = await _ocgisService.RetrieveOCTAPrioritizations();
            await SaveToStagingTable(octaPrioritizationFromEsris);
            await MergeAndProjectTo4326(dbContext);
        }
        
        private async Task SaveToStagingTable(IEnumerable<OCGISService.OCTAPrioritizationFromEsri> octaPrioritizationFromEsris)
        {
            var octaPrioritizationStagings = octaPrioritizationFromEsris.Select(x => new OCTAPrioritizationStaging()
                {
                    OCTAPrioritizationGeometry = x.Geometry,
                    OCTAPrioritizationKey = x.OCTAPrioritizationKey,
                    Watershed = x.Watershed,
                    CatchIDN = x.CatchIDN,
                    TPI = x.TPI,
                    WQNLU = x.WQNLU,
                    WQNMON = x.WQNMON,
                    IMPAIR = x.IMPAIR,
                    MON = x.MON,
                    SEA = x.SEA,
                    SEA_PCTL = x.SEA_PCTL,
                    PC_VOL_PCT = x.PC_VOL_PCT,
                    PC_NUT_PCT = x.PC_NUT_PCT,
                    PC_BAC_PCT = x.PC_BAC_PCT,
                    PC_MET_PCT = x.PC_MET_PCT,
                    PC_TSS_PCT = x.PC_TSS_PCT
                }).ToList();
            await DbContext.OCTAPrioritizationStagings.AddRangeAsync(octaPrioritizationStagings);
            await DbContext.SaveChangesAsync();
        }

        private static async Task MergeAndProjectTo4326(NeptuneDbContext dbContext)
        {
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pOCTAPrioritizationUpdateFromStaging");
            foreach (var octaPrioritization in dbContext.OCTAPrioritizations)
            {
                octaPrioritization.OCTAPrioritizationGeometry4326 = octaPrioritization.OCTAPrioritizationGeometry.ProjectTo4326();
            }
            await dbContext.SaveChangesAsync();
        }
    }
}