using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Hangfire;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Services;
using System.Collections.Specialized;
using System.Globalization;

namespace Neptune.WebMvc.Controllers
{
    public class PowerBIController(
        NeptuneDbContext dbContext,
        ILogger<PowerBIController> logger,
        IOptions<WebConfiguration> webConfiguration,
        LinkGenerator linkGenerator,
        AzureBlobStorageService azureBlobStorageService)
        : NeptuneBaseController<PowerBIController>(dbContext, logger, linkGenerator, webConfiguration)
    {
        [HttpGet("{webServiceToken}")]
        [WebServiceNameAndDescription("Treatment Facility Parameterization",
                                            "This table can be joined to the ‘Treatment Facility Attributes’ table to indicate " +
                                            "if a facility is fully parameterized and ready to be computed in the Modeling Module. The BMP " +
                                            "Inventory and Modeling Module in the OCST website provide new indicators and alerts to help determine " +
                                            "which attributes are missing for a specific facility.")]
        public JsonResult TreatmentBMPParameterizationSummary([ParameterDescription("Authorization Token")] WebServiceToken webServiceToken)
        {
            var delineations = vTreatmentBMPUpstreams.ListWithDelineationAsDictionary(_dbContext);
            var treatmentBMPModelingAttributes = vTreatmentBMPModelingAttributes.ListAsDictionary(_dbContext);
            var data = TreatmentBMPs.GetNonPlanningModuleBMPs(_dbContext)
                .Where(x => x.TreatmentBMPType.IsAnalyzedInModelingModule).ToList().Select(x =>
                {
                    var isFullyParameterized = x.IsFullyParameterized(delineations[x.TreatmentBMPID], treatmentBMPModelingAttributes.TryGetValue(x.TreatmentBMPID, out var attribute) ? attribute : null) ? "Yes" : "No";
                    return new
                    {
                        x.TreatmentBMPID,
                        x.TreatmentBMPName,
                        x.TreatmentBMPType.TreatmentBMPTypeName,
                        FullyParameterized = isFullyParameterized,
                        IsReadyForModeling = isFullyParameterized
                    };
                });

            return Json(data);
        }


        [HttpGet("{webServiceToken}")]
        [WebServiceNameAndDescriptionAttribute("Treatment Facility Attributes, Centralized BMP Attributes",
                                "This table contains the Modeling Attributes that have been entered for each Facility. Each row is a single facility and" +
                                          " its physical attributes. Null values for a modeling parameter in this table does not necessarily indicate that the BMP is missing data. " +
                                          "This is because there are usually only a few parameters required for each bmp type, and because there are many types of BMPs in this table. " +
                                          "See the ‘Treatment Facility Parameterization’ table for the indicator that the BMP is missing data." + 
                                          "<br/><br/>" +
                                          "This table also includes additional information to help locate and filter the facilities such as lat / lon, watershed, and jurisdiction." +
                                          "<br/><br/>" +
                                          "The second table, ‘Centralized BMP Attrs’ is identical to the first, except it’s filtered to just the centralized facility delineation types. This " +
                                          "is to facilitate reporting calculations related to facility tributary area for which we make different calculations if the facility is centralized or " +
                                          "distributed.")]
        public JsonResult TreatmentBMPAttributeSummary([ParameterDescription("Authorization Token")] WebServiceToken webServiceToken)
        {
            var data = _dbContext.vPowerBITreatmentBMPs.Select(x => new TreatmentBMPForPowerBI
            {
                PrimaryKey = x.TreatmentBMPID,
                TreatmentBMPName = x.TreatmentBMPName,
                Jurisdiction = x.Jurisdiction,
                LocationLon = x.LocationLon,
                LocationLat = x.LocationLat,
                Watershed = x.Watershed,
                DelineationType = x.DelineationType,
                WaterQualityManagementPlanID = x.WaterQualityManagementPlanID,
                TreatmentBMPID = x.TreatmentBMPID,
                TreatmentBMPTypeName = x.TreatmentBMPTypeName,
                UpstreamTreatmentBMPID = x.UpstreamTreatmentBMPID,
                AverageDivertedFlowrate = x.AverageDivertedFlowrate,
                AverageTreatmentFlowrate = x.AverageTreatmentFlowrate,
                DesignDryWeatherTreatmentCapacity = x.DesignDryWeatherTreatmentCapacity,
                DesignLowFlowDiversionCapacity = x.DesignLowFlowDiversionCapacity,
                DesignMediaFiltrationRate = x.DesignMediaFiltrationRate,
                DiversionRate = x.DiversionRate,
                DrawdownTimeforWQDetentionVolume = x.DrawdownTimeForWQDetentionVolume,
                EffectiveFootprint = x.EffectiveFootprint,
                EffectiveRetentionDepth = x.EffectiveRetentionDepth,
                InfiltrationDischargeRate = x.InfiltrationDischargeRate,
                InfiltrationSurfaceArea = x.InfiltrationSurfaceArea,
                MediaBedFootprint = x.MediaBedFootprint,
                PermanentPoolorWetlandVolume = x.PermanentPoolOrWetlandVolume,
                RoutingConfiguration = x.RoutingConfiguration ?? RoutingConfiguration.Online.RoutingConfigurationDisplayName,
                StorageVolumeBelowLowestOutletElevation = x.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = x.SummerHarvestedWaterDemand,
                TimeOfConcentration = x.TimeOfConcentration ?? TimeOfConcentration.FiveMinutes.TimeOfConcentrationDisplayName,
                DrawdownTimeForDetentionVolume = x.DrawdownTimeForDetentionVolume,
                TotalEffectiveBMPVolume = x.TotalEffectiveBMPVolume,
                TotalEffectiveDrywellBMPVolume = x.TotalEffectiveDrywellBMPVolume,
                TreatmentRate = x.TreatmentRate,
                UnderlyingHydrologicSoilGroup = x.UnderlyingHydrologicSoilGroup,
                UnderlyingInfiltrationRate = x.UnderlyingInfiltrationRate,
                WaterQualityDetentionVolume = x.WaterQualityDetentionVolume,
                WettedFootprint = x.WettedFootprint,
                WinterHarvestedWaterDemand = x.WinterHarvestedWaterDemand
            });

            return Json(data);
        }

        [HttpGet("{webServiceToken}")]
        [WebServiceNameAndDescriptionAttribute("WQMP Attributes",
                                                "This table includes summary attributes of WQMP sites helpful for filtering and " +
                                                "reporting, and each row is a single WQMP project site. Additional summary attributes may " +
                                                "be added to this table in the future.  In its current form (as of March, 2020) this table " +
                                                "does not yet support identification of treatment achieved via entries to the Other Structural " +
                                                "Facility table available in the OCST interface, but standalone BMPs in the ‘Treatment Facility" +
                                                " Attributes’ do store an association to a WQMP if such an association exists.  Future reporting " +
                                                "capabilities are planned to include treatment accounted for in the Other Structural Facility interface.")]
        public JsonResult WaterQualityManagementPlanAttributeSummary([ParameterDescription("Authorization Token")] WebServiceToken webServiceToken)
        {
            var data = _dbContext.vPowerBIWaterQualityManagementPlans.ToList().Select(x => new
            {
                x.WaterQualityManagementPlanID,
                Name = x.WaterQualityManagementPlanName,
                Jurisdiction = x.OrganizationName,
                Status = x.WaterQualityManagementPlanStatusDisplayName,
                DevelopmentType = x.WaterQualityManagementPlanDevelopmentTypeDisplayName,
                LandUse = x.WaterQualityManagementPlanLandUseDisplayName,
                PermitTerm = x.WaterQualityManagementPlanPermitTermDisplayName,
                x.ApprovalDate,
                x.DateOfConstruction,
                HydromodificationApplies = x.HydromodificationAppliesDisplayName,
                HydrologicSubarea = x.HydrologicSubareaName,
                x.RecordedWQMPAreaInAcres,
                TrashCaptureStatus = x.TrashCaptureStatusTypeDisplayName,
                x.TrashCaptureEffectiveness,
                x.ModelingApproach
            });

            return Json(data);
        }

        [HttpGet("{webServiceToken}")]
        [WebServiceNameAndDescriptionAttribute("Water Quality Management Plan O&M Verifications", "An inventory of O&M Verification visits for Water Quality Management Plans (WQMPs)")]
        public JsonResult WaterQualityManagementPlanOAndMVerifications([ParameterDescription("Authorization Token")] WebServiceToken webServiceToken)
        {
            var data = _dbContext.vPowerBIWaterQualityManagementPlanOAndMVerifications.ToList().Select(x => new
                {
                    WQMPID = x.PrimaryKey,
                    x.WQMPName,
                    x.Jurisdiction,
                    VerificationDate = x.VerificationDate.ToString(CultureInfo.InvariantCulture),
                    LastEditedDate = x.LastEditedDate.ToString(CultureInfo.InvariantCulture),
                    x.LastEditedBy,
                    x.TypeOfVerification,
                    x.VisitStatus,
                    x.VerificationStatus,
                    x.SourceControlCondition,
                    x.EnforcementOrFollowupActions,
                    x.DraftOrFinalized
                });

            //JsonResult serializes ampersands to be their unicode values because it uses JavascriptSerializer
            //JsonConvert does not
            //Returning this way to protect the MANY ampersands we encounter in this particular method
            return Json(data);
        }


        [HttpGet("{webServiceToken}")]
        [WebServiceNameAndDescriptionAttribute("Land Use",
                                                "This table is the result of a spatial overlay analysis (union) between the Regional " +
                                                "Subbasins managed by OC Survey, WQMP project boundaries entered into OCST, and distributed " +
                                                "delineations also entered into OCST. The result is a **non-overlapping** account of how the " +
                                                "land surface is classified in the OCST system. These OCST classes are further subdivided by additional " +
                                                "spatial analysis (provided by a web service build by OC Survey) to identify hydrologic soil group (HSG), " +
                                                "slope category (0, 5, 10+), land use, and to compute a % imperviousness for each resulting ‘sliver’ of the " +
                                                "landscape. This generates a very tall table in which each row is a sliver that identifies precisely how each " +
                                                "sliver is treated, and how each sliver might assert influence on the hydrology of the system. " + 
                                                "<br/>" +
                                                "The PowerBI file uses the ID columns(TreatmentBMPID, WaterQualityManagementPlanID) to group these slivers into pivot " +
                                                "tables to report total area treated, land use composition and % impervious.")]
        public async Task<IActionResult> LandUseStatistics([ParameterDescription("Authorization Token")] WebServiceToken webServiceToken)
        {
            return await DisplayFileResource(HRURefreshJob.LandUseStatisticsFileName);
        }

        [HttpGet("{webServiceToken}")]
        [WebServiceNameAndDescriptionAttribute("Centralized BMP Land Use Relationship",
                                                "This table is a utility table to enable upstream summary reporting of " +
                                                "centralized facilities. Centralized facilities are a special case, since the area " +
                                                "they treat may also be treated by a distributed facility, or a WQMP, or even by " +
                                                "another centralized BMP upstream. This table is the result of visiting each centralized " +
                                                "facility and checking which “slivers” from the ‘Land Use’ table are upstream of the " +
                                                "current centralized facility. This relationship allows the report to accurately aggregate " +
                                                "the area treated by each centralized facility individually. This relationship drives the " +
                                                "upstream area calculations on the Centralized Facilities dashboard of the PowerBI file (dated March, 2020).")]
        public JsonResult CentralizedBMPLoadGeneratingUnitMapping([ParameterDescription("Authorization Token")] WebServiceToken webServiceToken)
        {
            var data = _dbContext.vPowerBICentralizedBMPLoadGeneratingUnits.Select(x => new
            {
                x.LoadGeneratingUnitID,
                x.TreatmentBMPID
            });

            return Json(data);
        }

        [HttpGet("{webServiceToken}")]
        [WebServiceNameAndDescriptionAttribute("Model Results",
            "Returns all pollutant runoff/reduction model results for all nodes in South Orange County.")]
        public async Task<IActionResult> ModelResults([ParameterDescription("Authorization Token")] WebServiceToken webServiceToken)
        {
            return await DisplayFileResource(TotalNetworkSolveJob.ModelResultsFileName);
        }

        private async Task<IActionResult> DisplayFileResource(string fileName)
        {
            if (await azureBlobStorageService.ExistsFromBlobStorage(fileName))
            {
                var contentDisposition = new System.Net.Mime.ContentDisposition
                {
                    FileName = fileName,
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

                var blobDownloadResult = await azureBlobStorageService.DownloadBlobFromBlobStorage(fileName);

                return File(blobDownloadResult.Content.ToArray(), blobDownloadResult.Details.ContentType);
            }
            // Unhappy path - return an HTTP 404
            // ---------------------------------
            _logger.LogError($"File {fileName} Not Found. It may have been deleted.");
            return new NotFoundResult();
        }

        [HttpGet("{webServiceToken}")]
        [WebServiceNameAndDescriptionAttribute("Baseline Model Results", "Returns all pollutant runoff/reduction model results for all nodes in South Orange County in the Baseline Condition.")]
        public async Task<IActionResult> BaselineModelResults([ParameterDescription("Authorization Token")] WebServiceToken webServiceToken)
        {
            return await DisplayFileResource(TotalNetworkSolveJob.BaselineModelResultsFileName);
        }
    }

    public class TreatmentBMPForPowerBI
    {
        public int PrimaryKey { get; set; }
        public double? LocationLon { get; set; }
        public double? LocationLat { get; set; }
        public string Watershed { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int? UpstreamTreatmentBMPID { get; set; }
        public double? AverageDivertedFlowrate { get; set; }
        public double? AverageTreatmentFlowrate { get; set; }
        public double? DesignDryWeatherTreatmentCapacity { get; set; }
        public double? DesignLowFlowDiversionCapacity { get; set; }
        public double? DesignMediaFiltrationRate { get; set; }
        public double? DiversionRate { get; set; }
        public double? DrawdownTimeforWQDetentionVolume { get; set; }
        public double? EffectiveFootprint { get; set; }
        public double? EffectiveRetentionDepth { get; set; }
        public double? InfiltrationDischargeRate { get; set; }
        public double? InfiltrationSurfaceArea { get; set; }
        public double? MediaBedFootprint { get; set; }
        public double? PermanentPoolorWetlandVolume { get; set; }
        public string? RoutingConfiguration { get; set; }
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }
        public double? SummerHarvestedWaterDemand { get; set; }
        public string? TimeOfConcentration { get; set; }
        public double? DrawdownTimeForDetentionVolume { get; set; }
        public double? TotalEffectiveBMPVolume { get; set; }
        public double? TotalEffectiveDrywellBMPVolume { get; set; }
        public double? TreatmentRate { get; set; }
        public string? UnderlyingHydrologicSoilGroup { get; set; }
        public double? UnderlyingInfiltrationRate { get; set; }
        public double? WaterQualityDetentionVolume { get; set; }
        public double? WettedFootprint { get; set; }
        public double? WinterHarvestedWaterDemand { get; set; }
        public string DelineationType { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public string TreatmentBMPName { get; set; }
        public string Jurisdiction { get; set; }
    }
}