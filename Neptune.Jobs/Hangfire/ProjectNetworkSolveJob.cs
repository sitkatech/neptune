using System.Diagnostics;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Common.Email;
using Neptune.Common.Services;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels.Entities;
using Neptune.Jobs.EsriAsynchronousJob;
using Neptune.Jobs.Services;

namespace Neptune.Jobs.Hangfire
{
    public class ProjectNetworkSolveJob
    {
        public const string JobName = "Nereid Planned Project Network Solve";

        private readonly NereidService _nereidService;
        private readonly OCGISService _ocgisService;
        private readonly QGISAPIService _qgisApiService;
        private readonly ILogger<ProjectNetworkSolveJob> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly NeptuneDbContext _dbContext;
        private readonly NeptuneJobConfiguration _neptuneJobConfiguration;
        private readonly SitkaSmtpClientService _sitkaSmtpClient;

        public ProjectNetworkSolveJob(ILogger<ProjectNetworkSolveJob> logger, IWebHostEnvironment webHostEnvironment, NeptuneDbContext dbContext,
            IOptions<NeptuneJobConfiguration> neptuneJobConfiguration, SitkaSmtpClientService sitkaSmtpClientService, NereidService nereidService, OCGISService ocgisService, QGISAPIService qgisApiService)
        {
            _nereidService = nereidService;
            _ocgisService = ocgisService;
            _qgisApiService = qgisApiService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
            _neptuneJobConfiguration = neptuneJobConfiguration.Value;
            _sitkaSmtpClient = sitkaSmtpClientService;
        }

        public async Task RunNetworkSolveForProject(int projectID, int projectNetworkSolveHistoryID)
        {
            var project = _dbContext.Projects.AsNoTracking().SingleOrDefault(x => x.ProjectID == projectID);
            if (project == null)
            {
                throw new NullReferenceException($"Project with ID {projectID} does not exist!");
            }
            var projectNetworkSolveHistory = _dbContext.ProjectNetworkSolveHistories.Include(x => x.RequestedByPerson).First(x => x.ProjectNetworkSolveHistoryID == projectNetworkSolveHistoryID);
            var treatmentBMPs = _dbContext.TreatmentBMPs.Include(x => x.Delineation).AsNoTracking().Where(x => x.ProjectID == projectID);
            var projectRegionalSubbasinIDs = treatmentBMPs.Select(x => x.RegionalSubbasinID).Distinct().ToList();

            var regionalSubbasinIDs = _dbContext.vRegionalSubbasinUpstreams.AsNoTracking()
                .Where(x => projectRegionalSubbasinIDs.Contains(x.PrimaryKey) && x.RegionalSubbasinID.HasValue).Select(x => x.RegionalSubbasinID.Value).ToList();
            var projectDistributedDelineationIDs = treatmentBMPs.Select(x => x.Delineation).Where(x => x.DelineationTypeID == (int)DelineationTypeEnum.Distributed).Select(x => x.DelineationID).ToList();
            try
            {
                //Get our LGUs
                await LoadGeneratingUnitRefreshImpl(projectID, regionalSubbasinIDs);
                //Get our HRUs
                await HRURefreshImpl(projectID);
                await _nereidService.ProjectNetworkSolve(_dbContext, false, projectID, regionalSubbasinIDs, projectDistributedDelineationIDs);
                projectNetworkSolveHistory.ProjectNetworkSolveHistoryStatusTypeID = (int)ProjectNetworkSolveHistoryStatusTypeEnum.Succeeded;
                projectNetworkSolveHistory.LastUpdated = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
                await SendProjectNetworkSolveTerminalStatusEmail(projectNetworkSolveHistory.RequestedByPerson, project, true, null);
            }
            catch (Exception ex)
            {
                projectNetworkSolveHistory.ProjectNetworkSolveHistoryStatusTypeID = (int)ProjectNetworkSolveHistoryStatusTypeEnum.Failed;
                projectNetworkSolveHistory.LastUpdated = DateTime.UtcNow;
                projectNetworkSolveHistory.ErrorMessage = ex.Message;
                await _dbContext.SaveChangesAsync();
                await SendProjectNetworkSolveTerminalStatusEmail(projectNetworkSolveHistory.RequestedByPerson, project, false, ex.Message);
                throw;
            }
        }

        private async Task SendProjectNetworkSolveTerminalStatusEmail(Person requestPerson,
            Project project, bool successful, string errorMessage)
        {
            var projectName = project.ProjectName;
            var subject = successful ? $"Modeled Results calculated for Project: {projectName}" : $"Model Results calculation failed for Project: {projectName}";
            var requestPersonEmail = requestPerson.Email;
            var errorContext = $"<br /><br/>See the provided error message for more details:\n {errorMessage}";
            var planningURL = $"{_neptuneJobConfiguration.PlanningModuleBaseUrl}/projects/edit/{project.ProjectID}/stormwater-treatments/modeled-performance-and-metrics";
            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
<strong>{subject}</strong><br />
<br />
Model results calculation for Project:{projectName} has completed{(!successful ? " but encountered errors." : ".")}
<br /><br />
You can view the results or trigger another network solve <a href='{planningURL}'>here</a>.

{(!successful ? errorContext : "")}
";
            // Create Notification
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_neptuneJobConfiguration.DoNotReplyEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(requestPersonEmail);

            if (!successful)
            {
                foreach (var email in People.GetEmailAddressesForAdminsThatReceiveSupportEmails(_dbContext))
                {
                    mailMessage.CC.Add(email);
                }
            }

            await _sitkaSmtpClient.Send(mailMessage);
        }

        private async Task LoadGeneratingUnitRefreshImpl(int projectID, List<int> regionalSubbasinIDs)
        {
            await _qgisApiService.GeneratePLGUs(new GenerateProjectLoadGeneratingUnitRequestDto() { ProjectID = projectID, RegionalSubbasinIDs = regionalSubbasinIDs });
        }

        private async Task HRURefreshImpl(int projectID)
        {
            // collect the load generating units that require updates, which will be all for the Project
            // group them by Model basin so requests to the HRU service are spatially bounded. It is possible that a number of these won't have a model basin, but if the system is used in a logical manner any of these that don't have model basins will be in an rsb that is in North OC, so technically will still be spatially bounded
            // and batch them for processing 25 at a time so requests are small.
            _logger.LogInformation($"Processing '{JobName}'-HRURefresh for {projectID}");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var loadGeneratingUnitsToUpdate = _dbContext.ProjectLoadGeneratingUnits.Where(x => x.ProjectID == projectID && x.ProjectLoadGeneratingUnitGeometry.Area >= 10).ToList();
            var loadGeneratingUnitsToUpdateGroupedByModelBasin = loadGeneratingUnitsToUpdate.GroupBy(x => x.ModelBasinID);

            foreach (var group in loadGeneratingUnitsToUpdateGroupedByModelBasin)
            {
                var batches = group.Chunk(25);

                foreach (var batch in batches)
                {
                    try
                    {
                        var projectLoadGeneratingUnits = batch.ToList();
                        var hruRequestFeatures = HruRequestFeatureHelpers.GetHRURequestFeatures(projectLoadGeneratingUnits.ToDictionary(x => x.ProjectLoadGeneratingUnitID, x => x.ProjectLoadGeneratingUnitGeometry), true);
                        var hruResponseFeatures = await _ocgisService.RetrieveHRUResponseFeatures(hruRequestFeatures.ToList());

                        if (!hruResponseFeatures.Any())
                        {
                            foreach (var loadGeneratingUnit in projectLoadGeneratingUnits)
                            {
                                loadGeneratingUnit.IsEmptyResponseFromHRUService = true;
                            }
                            _logger.LogWarning($"No data for ProjectLGUs with these IDs: {string.Join(", ", projectLoadGeneratingUnits.Select(x => x.ProjectLoadGeneratingUnitID.ToString()))}");
                        }

                        var projectHruCharacteristics = hruResponseFeatures.Select(x =>
                        {
                            var hruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.Single(y => y.HRUCharacteristicLandUseCodeName == x.Attributes.ModelBasinLandUseDescription);
                            var baselineHruCharacteristicLandUseCode = HRUCharacteristicLandUseCode.All.Single(y => y.HRUCharacteristicLandUseCodeName == x.Attributes.BaselineLandUseDescription);

                            var projectHRUCharacteristic = new ProjectHRUCharacteristic
                            {
                                ProjectLoadGeneratingUnitID = x.Attributes.QueryFeatureID,
                                ProjectID = projectID,
                                HydrologicSoilGroup = x.Attributes.HydrologicSoilGroup,
                                SlopePercentage = x.Attributes.SlopePercentage.GetValueOrDefault(),
                                ImperviousAcres = x.Attributes.ImperviousAcres.GetValueOrDefault(),
                                LastUpdated = DateTime.UtcNow,
                                Area = x.Attributes.Acres.GetValueOrDefault(),
                                HRUCharacteristicLandUseCodeID = hruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID,
                                BaselineImperviousAcres = x.Attributes.BaselineImperviousAcres.GetValueOrDefault(),
                                BaselineHRUCharacteristicLandUseCodeID = baselineHruCharacteristicLandUseCode.HRUCharacteristicLandUseCodeID
                            };
                            return projectHRUCharacteristic;
                        }).ToList();
                        await _dbContext.ProjectHRUCharacteristics.AddRangeAsync(projectHruCharacteristics);
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        // this batch failed, but we don't want to give up the whole job.
                        _logger.LogWarning(ex.Message);
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

            stopwatch.Stop();
        }
    }
}
