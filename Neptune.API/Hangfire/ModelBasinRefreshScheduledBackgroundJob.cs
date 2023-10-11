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
    public class ModelBasinRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase<ModelBasinRefreshScheduledBackgroundJob>
    {
        public const string JobName = "Model Basin Refresh";

        private readonly OCGISService _ocgisService;

        public int PersonID { get; set; }

        public ModelBasinRefreshScheduledBackgroundJob(ILogger<ModelBasinRefreshScheduledBackgroundJob> logger,
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
            await dbContext.ModelBasinStagings.ExecuteDeleteAsync();
            var modelBasinFromEsris = await _ocgisService.RetrieveModelBasins();
            await SaveToStagingTable(modelBasinFromEsris);
            await MergeAndUpdateTreatmentBMPProperties(dbContext);
        }

        private async Task SaveToStagingTable(IEnumerable<OCGISService.ModelBasinFromEsri> modelBasinFromEsris)
        {
            var modelBasinStagings = modelBasinFromEsris.Select(x => new ModelBasinStaging()
            {
                ModelBasinGeometry = x.Geometry,
                ModelBasinKey = x.ModelBasinKey,
                ModelBasinState = x.ModelBasinState,
                ModelBasinRegion = x.ModelBasinRegion
            }).ToList();
            await DbContext.ModelBasinStagings.AddRangeAsync(modelBasinStagings);
            await DbContext.SaveChangesAsync();
        }
        
        private static async Task MergeAndUpdateTreatmentBMPProperties(NeptuneDbContext dbContext)
        {
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pModelBasinUpdateFromStaging");
            await dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.pTreatmentBMPUpdateModelBasin");
        }
    }
}